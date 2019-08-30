﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Core;
using Common.Extensions;
using Common.FeatureEntities;
using Common.Features.BaseEntity;
using Common.Features.Specifications;
using Common.Features.Tenant;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore.Internal;

namespace Common.Features.Cruds
{
    public class CrudControllerConfgurator<TEntity>: ICrudControllerConfgurator
        where TEntity : BaseEntityGuidKey
    {
        private readonly HashSet<ProjectRoles> _readRoles = new HashSet<ProjectRoles>();

        private readonly HashSet<ProjectRoles> _modifyRoles = new HashSet<ProjectRoles>();

        private readonly IList<Type> _mandatorySpecifications =new List<Type>();

        private readonly IList<Type> _entityCorrectorTypes = new List<Type>();

        private Type _entityValidator = null;

        public CrudControllerConfgurator()
        {
            AddMandatorySpecification<IsNotDeletedSpecification<TEntity>>();

            if (typeof(TEntity).HasImplementation<ITenantedEntity>())
            {
                AddMandatorySpecification<OnlyTenantEntitiesSpecification<TEntity>>();
                AddEntityCorrector<TenantedEntityCorrector>();
            }

            _entityValidator = typeof(EmptyValidator);
        }

        public CrudControllerConfgurator<TEntity> AddReadRoles(params ProjectRoles[] roles)
        {
            AppendRoles(_readRoles, roles);
            return this;
        }

        public CrudControllerConfgurator<TEntity> AddModifyRoles(params ProjectRoles[] roles)
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

        public CrudControllerConfgurator<TEntity> AddMandatorySpecification<TMandatorySpec>()
            where TMandatorySpec: ISpecification<TEntity>
        {
            _mandatorySpecifications.Add(typeof(TMandatorySpec));
            return this;
        }

        public CrudControllerConfgurator<TEntity> AddEntityCorrector<TCorrector>()
            where TCorrector : IEntityCorrector<TEntity>
        {
            _entityCorrectorTypes.Add(typeof(TCorrector));
            return this;
        }

        public CrudControllerConfgurator<TEntity> UseValidator<TValidator>()
            where TValidator: BaseEntityGuidKeyValidator<TEntity>
        {
            _entityValidator = typeof(TValidator);
            return this;
        }

        public void Finish(ContainerBuilder builder)
        {
            if (_entityValidator !=typeof(EmptyValidator))
            {
                builder.RegisterType(_entityValidator).AsSelf();
            }
            
            builder.RegisterType<CrudControllerDescriptor<TEntity>>()
                .As<ICrudControllerDescriptor>()
                .WithParameter("entityDescriptor", new CrudEntityDescriptor<TEntity>())
                .WithParameter("accessRuleMap", new AccessRuleMap(_readRoles.ToArray(),_modifyRoles.ToArray()))
                .WithParameter("mandatorySpecificationTypes", _mandatorySpecifications)
                .WithParameter("entityValidatorType", _entityValidator)
                .WithParameter("entityCorrectorTypes", _entityCorrectorTypes)
                .SingleInstance();

            //new CrudControllerDescriptor(new CrudEntityDescriptor<TEntity>(), new AccessRuleMap(_readRoles.ToArray(), _modifyRoles.ToArray()));
        }
    }
}
