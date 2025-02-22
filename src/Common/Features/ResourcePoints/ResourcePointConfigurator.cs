﻿using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Autofac;
using Common.Extensions;
using Common.Features.BaseEntity;
using Common.Features.ResourcePoints.Crud;
using Common.Features.ResourcePoints.Filterable;
using Common.Features.ResourcePoints.Filterable.Schema;
using Common.Features.Specifications;
using Common.Features.Tenant;
using EntityContextFilterOperationParameters = Common.Features.ResourcePoints.Filterable.EntityContextFilterOperationParameters;
using EntityFilterOperationParameters = Common.Features.ResourcePoints.Filterable.EntityFilterOperationParameters;
using EntitySqlFilterOperationParameters = Common.Features.ResourcePoints.Filterable.EntitySqlFilterOperationParameters;

namespace Common.Features.ResourcePoints
{
	public class ResourcePointConfigurator<TEntity, TFilterableEntity, TKey>: IResourcePointConfigurator
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TKey:struct
	{
		private Type _filterableEntityOperation = null;

		private EntityFilterOperationParameters _filterableEntityOperationParameters = EntityFilterOperationParameters.Empty;

		private string _entityPluralName;

		protected readonly HashSet<ProjectRoles> _readRoles = new HashSet<ProjectRoles>();

		private readonly IList<Type> _mandatorySpecifications = new List<Type>();

		private Type _entitySchemaProviderType;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public ResourcePointConfigurator()
		{
			AddMandatorySpecification<IsNotDeletedSpecification<TEntity,TKey>>();

			if (typeof(TEntity).HasImplementation<ITenantedEntity>())
			{
				AddMandatorySpecification<OnlyTenantEntitiesSpecification<TEntity,TKey>>();
				//_entityCorrectorTypes.Add(typeof(TenantedEntityCorrector));
			}
		}

		public ResourcePointConfigurator<TEntity, TFilterableEntity, TKey> UseFilterableEntityOperation<TFilterableOperation>(Action<EntityContextFilterOperationParameters> config)
			where TFilterableOperation : EntityContextFilterOperation<TEntity, TFilterableEntity,TKey>
		{
			var parameters = new EntityContextFilterOperationParameters();
			config(parameters);
			_filterableEntityOperationParameters = parameters;

			_filterableEntityOperation = typeof(TFilterableOperation);
			return this;
		}

		public ResourcePointConfigurator<TEntity, TFilterableEntity, TKey> UseFilterableEntityOperation<TFilterableOperation>(Action<EntitySqlFilterOperationParameters> config)
			where TFilterableOperation : EntitySqlFilterOperation<TEntity, TFilterableEntity, TKey>
		{
			_filterableEntityOperation = typeof(TFilterableOperation);
			var parameters = new EntitySqlFilterOperationParameters();
			config(parameters);
			_filterableEntityOperationParameters = parameters;
			return this;
		}

		public ResourcePointConfigurator<TEntity, TFilterableEntity, TKey> SetEntityPluralName(string pluralName)
		{
			_entityPluralName = pluralName;
			return this;
		}

		public ResourcePointConfigurator<TEntity, TFilterableEntity, TKey> AddReadRoles(params ProjectRoles[] roles)
		{
			AppendRoles(_readRoles, roles);
			return this;
		}

		public ResourcePointConfigurator<TEntity, TFilterableEntity, TKey> AddMandatorySpecification<TMandatorySpec>()
			where TMandatorySpec : ISpecification<TEntity>
		{
			_mandatorySpecifications.Add(typeof(TMandatorySpec));
			return this;
		}

		//SchemaProvider IEntitySchemaProvider
		public ResourcePointConfigurator<TEntity, TFilterableEntity, TKey> UseEntitySchemaProvider<TProvider>()
		where TProvider: IEntitySchemaProvider<TFilterableEntity>
		{
			_entitySchemaProviderType = typeof(TProvider);
			return this;
		}

		public void Finish(ContainerBuilder builder)
		{
			if (_filterableEntityOperation == null)
			{
				throw new InvalidOperationException("Need to call UseFilterableEntityOperation");
			}

			var parameters = CreateFactoryParameters();
			FillControllerFactoryParameters(parameters);

			var listBaseTypes = new List<Type>();
			var factoryType = GetResourcePointFactoryType(listBaseTypes);
			builder.RegisterType(factoryType)
				.As<IResourcePointControllerFactory>()
				.AsSelf()
				.As(listBaseTypes.ToArray())
				.WithParameter("parameters", parameters)
				.SingleInstance();

			if (_entitySchemaProviderType!=null)
			{
				builder.RegisterType(_entitySchemaProviderType).AsSelf().SingleInstance();
			}
			RegisterTypes(builder);
		}

		protected virtual void RegisterTypes(ContainerBuilder builder)
		{
			
		}

		protected virtual Type GetResourcePointFactoryType(List<Type> listBaseTypes)
		{
			return typeof(ResourcePointControllerFactory<TEntity, TFilterableEntity, TKey>);
		}
		protected virtual ControllerFactoryParameters CreateFactoryParameters()
		{
			return new ControllerFactoryParameters();
		}

		protected virtual void FillControllerFactoryParameters(ControllerFactoryParameters parameters)
		{
			var resourcePointDescriptor = new ResourcePointDescriptor();
			resourcePointDescriptor.EntityName = typeof(TEntity).Name;

			if (string.IsNullOrWhiteSpace(_entityPluralName))
			{
				resourcePointDescriptor.EntityPluralName = resourcePointDescriptor.EntityName + "s";
			}
			else
			{
				resourcePointDescriptor.EntityPluralName = _entityPluralName;
			}

			resourcePointDescriptor.FilterableEntityTypeInfo = typeof(TFilterableEntity);
			resourcePointDescriptor.EntityTypeInfo = typeof(TEntity);
			resourcePointDescriptor.KeyType = typeof(TKey);

			var controllerType = typeof(ResourcePointBaseController<,,>).MakeGenericType(typeof(TEntity),
				typeof(TFilterableEntity), typeof(TKey));
			parameters.ControllerType = controllerType;
			parameters.ResourcePoint = resourcePointDescriptor;
			parameters.AccessRuleMap = new AccessRuleMap(_readRoles.ToArray());
			parameters.FilterableEntityOperationType = _filterableEntityOperation;
			parameters.FilterableEntityOperationParameters = _filterableEntityOperationParameters;
			parameters.MandatorySpecificationTypes = _mandatorySpecifications;
			parameters.EntitySchemaProviderType = _entitySchemaProviderType;
		}

		protected void AppendRoles(HashSet<ProjectRoles> roles, ProjectRoles[] rolesToAppend)
		{
			foreach (var projectRole in rolesToAppend)
			{
				if (!roles.Contains(projectRole))
				{
					roles.Add(projectRole);
				}
			}
		}
	}
}
