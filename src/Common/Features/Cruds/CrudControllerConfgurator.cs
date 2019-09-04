using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Core;
using Common.Extensions;
using Common.FeatureEntities;
using Common.Features.BaseEntity;
using Common.Features.Cruds.Filterable;
using Common.Features.Specifications;
using Common.Features.Tenant;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore.Internal;

namespace Common.Features.Cruds
{
    public class CrudControllerConfgurator<TEntity,TEntityDto, TFilterableEntity> : ICrudControllerConfgurator
        where TEntity : BaseEntityGuidKey
    {
        private readonly HashSet<ProjectRoles> _readRoles = new HashSet<ProjectRoles>();

        private readonly HashSet<ProjectRoles> _modifyRoles = new HashSet<ProjectRoles>();

        private readonly IList<Type> _mandatorySpecifications =new List<Type>();

        private readonly IList<Type> _entityCorrectorTypes = new List<Type>();

        private Type _entityValidator = null;

        private Type _filterableEntityOperation = null;

        private EntityFilterOperationParameters _filterableEntityOperationParameters = EntityFilterOperationParameters.Empty;

        public CrudControllerConfgurator()
        {
            AddMandatorySpecification<IsNotDeletedSpecification<TEntity>>();

            if (typeof(TEntity).HasImplementation<ITenantedEntity>())
            {
                AddMandatorySpecification<OnlyTenantEntitiesSpecification<TEntity>>();
                _entityCorrectorTypes.Add(typeof(TenantedEntityCorrector));
            }

            _entityValidator = typeof(EmptyValidator);
        }

        public CrudControllerConfgurator<TEntity, TEntityDto, TFilterableEntity> AddReadRoles(params ProjectRoles[] roles)
        {
            AppendRoles(_readRoles, roles);
            return this;
        }

        public CrudControllerConfgurator<TEntity, TEntityDto, TFilterableEntity> AddModifyRoles(params ProjectRoles[] roles)
        {
            AppendRoles(_modifyRoles, roles);
            return this;
        }

        private void AppendRoles(HashSet<ProjectRoles> roles,ProjectRoles[] rolesToAppend)
        {
            foreach (var projectRole in rolesToAppend)
            {
                if (!roles.Contains(projectRole))
                {
                    roles.Add(projectRole);
                }
            }
        }

        public CrudControllerConfgurator<TEntity, TEntityDto, TFilterableEntity> AddMandatorySpecification<TMandatorySpec>()
            where TMandatorySpec: ISpecification<TEntity>
        {
            _mandatorySpecifications.Add(typeof(TMandatorySpec));
            return this;
        }

        public CrudControllerConfgurator<TEntity, TEntityDto, TFilterableEntity> AddEntityCorrector<TCorrector>()
            where TCorrector : IEntityCorrector<TEntity,TEntityDto>
        {
            _entityCorrectorTypes.Add(typeof(TCorrector));
            return this;
        }

        public CrudControllerConfgurator<TEntity, TEntityDto, TFilterableEntity> UseValidator<TValidator>()
            where TValidator: BaseEntityDtoValidator<TEntityDto>
        {
            _entityValidator = typeof(TValidator);
            return this;
        }

        public CrudControllerConfgurator<TEntity, TEntityDto, TFilterableEntity> UseFilterableEntityOperation<TFilterableOperation>()
            where TFilterableOperation: EntityContextFilterOperation<TEntity,TFilterableEntity>
        {
            _filterableEntityOperation = typeof(TFilterableOperation);
            return this;
        }

        public CrudControllerConfgurator<TEntity, TEntityDto, TFilterableEntity> UseFilterableEntityOperation<TFilterableOperation>(Action<EntitySqlFilterOperationParameters> config)
            where TFilterableOperation : EntitySqlFilterOperation<TEntity, TFilterableEntity>
        {
            _filterableEntityOperation = typeof(TFilterableOperation);
            var parameters = new EntitySqlFilterOperationParameters();
            config(parameters);
            _filterableEntityOperationParameters = parameters;
            return this;
        }

        public void Finish(ContainerBuilder builder)
        {
            if (_filterableEntityOperation==null)
            {
                throw new InvalidOperationException("Need to call UseFilterableEntityOperation");
            }

            if (_entityValidator !=typeof(EmptyValidator))
            {
                builder.RegisterType(_entityValidator).AsSelf();
            }
            
            builder.RegisterType<CrudControllerDescriptor<TEntity,TEntityDto,TFilterableEntity>>()
                .As<ICrudControllerDescriptor>()
                .WithParameter("entityDescriptor", new CrudEntityDescriptor<TEntity, TEntityDto, TFilterableEntity>())
                .WithParameter("accessRuleMap", new AccessRuleMap(_readRoles.ToArray(),_modifyRoles.ToArray()))
                .WithParameter("mandatorySpecificationTypes", _mandatorySpecifications)
                .WithParameter("entityValidatorType", _entityValidator)
                .WithParameter("entityCorrectorTypes", _entityCorrectorTypes)
                .WithParameter("filterableEntityOperationType", _filterableEntityOperation)
                .WithParameter("filterableEntityOperationParameters", _filterableEntityOperationParameters)
                .SingleInstance();
        }
    }
}
