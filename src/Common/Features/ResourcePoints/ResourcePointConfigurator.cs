using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Common.FeatureEntities;
using Common.Features.BaseEntity;
using Common.Features.Cruds;
using Common.Features.ResourcePoints.Filterable;

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

		public ResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey> UseFilterableEntityOperation<TFilterableOperation>(Action<EntityContextFilterOperationParameters> config)
			where TFilterableOperation : EntityContextFilterOperation<TEntity, TFilterableEntity,TKey>
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
