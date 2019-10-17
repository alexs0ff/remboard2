using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.FeatureEntities;
using Common.Features.BaseEntity;
using Common.Features.Cruds;
using Common.Features.ResourcePoints.Filterable;

namespace Common.Features.ResourcePoints
{
	public class ResourcePointControllerFactory<TEntity, TEntityDto, TFilterableEntity, TKey> : IResourcePointControllerFactory
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TEntityDto : class
		where TKey : struct
	{
		private readonly Type _filterableEntityOperationType;

		private readonly IComponentContext _context;

		private readonly EntityFilterOperationParameters _filterableEntityOperationParameters;

		public ResourcePointControllerFactory(IComponentContext context, IResourcePointDescriptor resourcePoint, Type controllerType, EntityFilterOperationParameters filterableEntityOperationParameters, Type filterableEntityOperationType, AccessRuleMap accessRuleMap)
		{
			ResourcePoint = resourcePoint;
			ControllerType = controllerType;
			_context = context;
			_filterableEntityOperationParameters = filterableEntityOperationParameters;
			_filterableEntityOperationType = filterableEntityOperationType;
			AccessRules = accessRuleMap;
		}

		public IResourcePointDescriptor ResourcePoint { get; }

		public Type ControllerType { get; }

		public AccessRuleMap AccessRules { get; }

		public string EntityName => ResourcePoint.EntityName;
	}
}
