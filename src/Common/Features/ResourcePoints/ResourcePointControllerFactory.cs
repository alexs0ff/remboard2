using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.FeatureEntities;
using Common.Features.BaseEntity;
using Common.Features.ResourcePoints.Filterable;
using Common.Features.ResourcePoints.Filterable.Schema;
using Common.Features.Specifications;
using Entities;

namespace Common.Features.ResourcePoints
{
	public class ResourcePointControllerFactory<TEntity,TFilterableEntity, TKey> : IResourcePointControllerFactory
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TKey : struct
	{
		private readonly Type _filterableEntityOperationType;

		private readonly IList<Type> _mandatorySpecificationTypes;

		protected readonly IComponentContext context;

		private readonly EntityFilterOperationParameters _filterableEntityOperationParameters;

		private readonly Type _entitySchemaProviderType;

		public ResourcePointControllerFactory(ControllerFactoryParameters parameters, IComponentContext context)
		{
			this.context = context;
			ResourcePoint = parameters.ResourcePoint;
			ControllerType = parameters.ControllerType;
			_filterableEntityOperationParameters = parameters.FilterableEntityOperationParameters;
			_filterableEntityOperationType = parameters.FilterableEntityOperationType;
			_mandatorySpecificationTypes = parameters.MandatorySpecificationTypes;
			AccessRules = parameters.AccessRuleMap;
			_entitySchemaProviderType = parameters.EntitySchemaProviderType;
		}

		public IResourcePointDescriptor ResourcePoint { get; }

		public Type ControllerType { get; }

		public AccessRuleMap AccessRules { get; }

		public string EntityName => ResourcePoint.EntityName;

		public IEntityFilterOperation<TEntity, TFilterableEntity, TKey> GetFilterableOperation()
		{
			return (IEntityFilterOperation<TEntity, TFilterableEntity,TKey>)context.Resolve(_filterableEntityOperationType, new NamedParameter("parameters", _filterableEntityOperationParameters));
		}

		public IResourceMandatoryPredicateFactory<TEntity, TKey> GetMandatoryPredicateFactory()
		{
			var list = new List<ISpecification<TEntity>>();
			foreach (var mandatorySpecification in _mandatorySpecificationTypes)
			{

				list.Add((ISpecification<TEntity>)context.Resolve(mandatorySpecification));
			}

			return new ResourceMandatoryPredicateFactory<TEntity, TKey>(list);
		}

		public IEntitySchemaProvider<TFilterableEntity> GetEntitySchemaProvider()
		{
			return (IEntitySchemaProvider<TFilterableEntity>) context.Resolve(_entitySchemaProviderType);
		}
	}
}
