using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Features.BaseEntity;
using Common.Features.ResourcePoints.Filterable;

namespace Common.Features.ResourcePoints
{
	public class ResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey, TController>: IResourcePointConfigurator
		where TController: ResourcePointBaseController<TEntity, TEntityDto, TFilterableEntity,TKey>
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TEntityDto : class
		where TKey:struct
	{
		private Type _filterableEntityOperation = null;

		private EntityFilterOperationParameters _filterableEntityOperationParameters = EntityFilterOperationParameters.Empty;

		public ResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey, TController> UseFilterableEntityOperation<TFilterableOperation>(Action<EntityContextFilterOperationParameters> config)
			where TFilterableOperation : EntityContextFilterOperation<TEntity, TFilterableEntity,TKey>
		{
			var parameters = new EntityContextFilterOperationParameters();
			config(parameters);
			_filterableEntityOperationParameters = parameters;

			_filterableEntityOperation = typeof(TFilterableOperation);
			return this;
		}

		public void Finish(ContainerBuilder builder)
		{
			
		}
	}
}
