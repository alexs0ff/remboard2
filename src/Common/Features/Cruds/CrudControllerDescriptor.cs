using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common.Features.BaseEntity;
using Common.Features.ErrorFlow;
using Common.Features.Specifications;
using FluentValidation;
using LinqKit;

namespace Common.Features.Cruds
{
    public class CrudControllerDescriptor<TEntity> : ICrudTypedControllerDescriptor<TEntity>
        where TEntity : BaseEntityGuidKey
    {
        private readonly ICrudEntityDescriptor _entityDescriptor;

        private readonly AccessRuleMap _accessRuleMap;

        private readonly IList<Type> _mandatorySpecificationTypes;

        private readonly Lazy<IList<ISpecification<TEntity>>> _mandatorySpecifications;

        /// <summary>
        /// IValidator because can use Empty validation
        /// </summary>
        private readonly Lazy<IValidator<TEntity>> _entityValidator;

        private readonly Type _entityValidatorType;

        private readonly IList<Type> _entityCorrectorTypes;

        private readonly IComponentContext _context;

        private readonly Lazy<IList<IEntityCorrector<TEntity>>> _entityCorrectors;

        public CrudControllerDescriptor(ICrudEntityDescriptor entityDescriptor, AccessRuleMap accessRuleMap, IList<Type> mandatorySpecificationTypes,IComponentContext context, Type entityValidatorType,IList<Type> entityCorrectorTypes)
        {
            _entityDescriptor = entityDescriptor;
            _accessRuleMap = accessRuleMap;
            _mandatorySpecificationTypes = mandatorySpecificationTypes;
            _context = context;
            _entityValidatorType = entityValidatorType;
            _entityCorrectorTypes = entityCorrectorTypes;

            _mandatorySpecifications = new Lazy<IList<ISpecification<TEntity>>>(SpecificationsFactory);

            _entityValidator = new Lazy<IValidator<TEntity>>(ValidatorFactory);

            _entityCorrectors = new Lazy<IList<IEntityCorrector<TEntity>>>(CorectorsFactory);
        }

        private IValidator<TEntity> ValidatorFactory()
        {
            return (IValidator<TEntity>)_context.Resolve(_entityValidatorType);
        }

        private IList<IEntityCorrector<TEntity>> CorectorsFactory()
        {
            var list = new List<IEntityCorrector<TEntity>>();

            foreach (var entityCorrectorType in _entityCorrectorTypes)
            {
                list.Add((IEntityCorrector<TEntity>)_context.Resolve(entityCorrectorType));
            }

            return list;
        }

        private IList<ISpecification<TEntity>> SpecificationsFactory()
        {
            var list = new List<ISpecification<TEntity>>();
            foreach (var mandatorySpecification in _mandatorySpecificationTypes)
            {

                list.Add((ISpecification<TEntity>)_context.Resolve(mandatorySpecification));
            }

            return list;

        }

        public string EntityName => _entityDescriptor.EntityName;

        public ICrudEntityDescriptor EntityDescriptor => _entityDescriptor;

        public AccessRuleMap AccessRules => _accessRuleMap;

        public ExpressionStarter<TEntity> GetMandatoryPredicate()
        {
            var predicate = LinqKit.PredicateBuilder.New<TEntity>(true);
            foreach (var mandatorySpecification in _mandatorySpecifications.Value)
            {
                predicate.And(mandatorySpecification.IsSatisfiedBy());
            }
            return predicate;
        }

        public async Task<ValidationErrorItem[]> ValidateAsync(TEntity entity)
        {
            var result = await _entityValidator.Value.ValidateAsync(entity);

            if (result.IsValid)
            {
                return null;
            }

            return result.Errors.Select(i => new ValidationErrorItem
                {Property = i.PropertyName, Message = i.ErrorMessage}).ToArray();
        }

        public async Task CorrectBeforeAsync(TEntity entity)
        {
            foreach (var entityCorrector in _entityCorrectors.Value)
            {
                await entityCorrector.CorrectBefore(entity);
            }
        }

        public async Task CorrectAfterAsync(TEntity entity)
        {
            foreach (var entityCorrector in _entityCorrectors.Value)
            {
                await entityCorrector.CorrectAfter(entity);
            }
        }
    }
}
