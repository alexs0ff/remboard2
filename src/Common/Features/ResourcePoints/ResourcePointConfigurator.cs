using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Common.Extensions;
using Common.FeatureEntities;
using Common.Features.BaseEntity;
using Common.Features.Cruds;
using Common.Features.ResourcePoints.Filterable;
using Common.Features.Specifications;
using Common.Features.Tenant;

namespace Common.Features.ResourcePoints
{
	public class ResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey>: IResourcePointConfigurator
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TEntityDto : class
		where TKey:struct
	{
		private Type _filterableEntityOperation = null;

		private EntityFilterOperationParameters _filterableEntityOperationParameters = EntityFilterOperationParameters.Empty;

		private string _entityPluralName;

		private readonly HashSet<ProjectRoles> _readRoles = new HashSet<ProjectRoles>();

		private readonly IList<Type> _mandatorySpecifications = new List<Type>();

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

		public ResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey> UseFilterableEntityOperation<TFilterableOperation>(Action<EntityContextFilterOperationParameters> config)
			where TFilterableOperation : IEntityFilterOperation<TEntity, TFilterableEntity,TKey>
		{
			var parameters = new EntityContextFilterOperationParameters();
			config(parameters);
			_filterableEntityOperationParameters = parameters;

			_filterableEntityOperation = typeof(TFilterableOperation);
			return this;
		}

		public ResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey> SetEntityPluralName(string pluralName)
		{
			_entityPluralName = pluralName;
			return this;
		}

		public ResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey> AddReadRoles(params ProjectRoles[] roles)
		{
			AppendRoles(_readRoles, roles);
			return this;
		}

		public ResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey> AddMandatorySpecification<TMandatorySpec>()
			where TMandatorySpec : ISpecification<TEntity>
		{
			_mandatorySpecifications.Add(typeof(TMandatorySpec));
			return this;
		}

		public void Finish(ContainerBuilder builder)
		{
			if (_filterableEntityOperation == null)
			{
				throw new InvalidOperationException("Need to call UseFilterableEntityOperation");
			}
			var resourcePointDescriptor = new ResourcePointDescriptor();
			resourcePointDescriptor.EntityDtoTypeInfo = typeof(TEntityDto);
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

			var controllerType = typeof(ResourcePointBaseController<,,,>).MakeGenericType(typeof(TEntity), typeof(TEntityDto),
				typeof(TFilterableEntity), typeof(TKey));

			builder.RegisterType<ResourcePointControllerFactory<TEntity,TEntityDto,TFilterableEntity,TKey>>()
				.As<IResourcePointControllerFactory>()
				.AsSelf()
				.WithParameter("resourcePoint", resourcePointDescriptor)
				.WithParameter("controllerType", controllerType)
				.WithParameter("accessRuleMap", new AccessRuleMap(_readRoles.ToArray()))
				.WithParameter("filterableEntityOperationType", _filterableEntityOperation)
				.WithParameter("filterableEntityOperationParameters", _filterableEntityOperationParameters)
				.WithParameter("mandatorySpecificationTypes", _mandatorySpecifications)
				.SingleInstance();
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
