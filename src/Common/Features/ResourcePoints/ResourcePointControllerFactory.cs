using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.FeatureEntities;
using Common.Features.BaseEntity;
using Common.Features.Cruds;
using Common.Features.ResourcePoints.Filterable;
using Common.Features.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Common.Features.ResourcePoints
{
	public class ResourcePointControllerFactory<TEntity, TEntityDto, TFilterableEntity, TKey> : IResourcePointControllerFactory
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TEntityDto : class
		where TKey : struct
	{
		private readonly Type _filterableEntityOperationType;

		private readonly IList<Type> _mandatorySpecificationTypes;

		private readonly IComponentContext _context;

		private readonly EntityFilterOperationParameters _filterableEntityOperationParameters;

		public ResourcePointControllerFactory(IComponentContext context, IResourcePointDescriptor resourcePoint, Type controllerType, EntityFilterOperationParameters filterableEntityOperationParameters, Type filterableEntityOperationType, AccessRuleMap accessRuleMap, IList<Type> mandatorySpecificationTypes)
		{
			ResourcePoint = resourcePoint;
			ControllerType = controllerType;
			_context = context;
			_filterableEntityOperationParameters = filterableEntityOperationParameters;
			_filterableEntityOperationType = filterableEntityOperationType;
			_mandatorySpecificationTypes = mandatorySpecificationTypes;
			AccessRules = accessRuleMap;
		}

		public IResourcePointDescriptor ResourcePoint { get; }

		public Type ControllerType { get; }

		public AccessRuleMap AccessRules { get; }

		public string EntityName => ResourcePoint.EntityName;

		public IEntityFilterOperation<TEntity, TFilterableEntity, TKey> GetFilterableOperation()
		{
			return (IEntityFilterOperation<TEntity, TFilterableEntity,TKey>)_context.Resolve(_filterableEntityOperationType, new NamedParameter("parameters", _filterableEntityOperationParameters));
		}

		public IResourceMandatoryPredicateFactory<TEntity, TKey> GetMandatoryPredicateFactory()
		{
			var list = new List<ISpecification<TEntity>>();
			foreach (var mandatorySpecification in _mandatorySpecificationTypes)
			{

				list.Add((ISpecification<TEntity>)_context.Resolve(mandatorySpecification));
			}

			return new ResourceMandatoryPredicateFactory<TEntity, TKey>(list);
		}
	}
}
