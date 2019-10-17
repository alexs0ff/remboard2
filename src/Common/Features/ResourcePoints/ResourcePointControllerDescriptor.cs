using System;
using System.Collections.Generic;
using System.Text;
using Common.FeatureEntities;
using Common.Features.BaseEntity;
using Common.Features.Cruds;
using Common.Features.ResourcePoints.Filterable;

namespace Common.Features.ResourcePoints
{
	public class ResourcePointControllerDescriptor<TEntity, TEntityDto, TFilterableEntity, TKey> : IResourcePointControllerDescriptor
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TEntityDto : class
		where TKey : struct
	{
		private readonly Type _filterableEntityOperationType;

		private readonly EntityFilterOperationParameters _filterableEntityOperationParameters;

		public ResourcePointControllerDescriptor(IResourcePointDescriptor resourcePoint, Type controllerType, EntityFilterOperationParameters filterableEntityOperationParameters, Type filterableEntityOperationType, AccessRuleMap accessRuleMap)
		{
			ResourcePoint = resourcePoint;
			ControllerType = controllerType;
			_filterableEntityOperationParameters = filterableEntityOperationParameters;
			_filterableEntityOperationType = filterableEntityOperationType;
			AccessRules = accessRuleMap;
		}

		public IResourcePointDescriptor ResourcePoint { get; }

		public Type ControllerType { get; }

		public AccessRuleMap AccessRules { get; }
	}
}
