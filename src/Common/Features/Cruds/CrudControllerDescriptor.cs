using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Features.BaseEntity;
using Common.Features.Specifications;
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

        private IList<ISpecification<TEntity>> SpecificationsFactory()
        {
            var list = new List<ISpecification<TEntity>>();
            foreach (var mandatorySpecification in _mandatorySpecificationTypes)
            {

                list.Add((ISpecification<TEntity>)_context.Resolve(mandatorySpecification));
            }

            return list;
           
        }

        private readonly IComponentContext _context;

        public CrudControllerDescriptor(ICrudEntityDescriptor entityDescriptor, AccessRuleMap accessRuleMap, IList<Type> mandatorySpecificationTypes,IComponentContext context)
        {
            _entityDescriptor = entityDescriptor;
            _accessRuleMap = accessRuleMap;
            _mandatorySpecificationTypes = mandatorySpecificationTypes;
            _context = context;

            _mandatorySpecifications = new Lazy<IList<ISpecification<TEntity>>>(() => SpecificationsFactory());
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
    }
}
