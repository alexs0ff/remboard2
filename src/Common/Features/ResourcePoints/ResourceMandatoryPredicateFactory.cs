using System;
using System.Collections.Generic;
using Entities;
using Common.Features.Specifications;
using LinqKit;

namespace Common.Features.ResourcePoints
{
	public class ResourceMandatoryPredicateFactory<TEntity,TKey>: IResourceMandatoryPredicateFactory<TEntity,TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : struct
	{
		private readonly List<ISpecification<TEntity>> _specifications;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public ResourceMandatoryPredicateFactory(List<ISpecification<TEntity>> specifications)
		{
			_specifications = specifications;
		}

		public ExpressionStarter<TEntity> GetMandatoryPredicates()
		{
			var predicate = LinqKit.PredicateBuilder.New<TEntity>(true);
			foreach (var mandatorySpecification in _specifications)
			{
				predicate.And(mandatorySpecification.IsSatisfiedBy());
			}
			return predicate;
		}
	}
}
