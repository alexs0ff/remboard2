using System;
using System.Collections.Generic;
using Entities;
using LinqKit;

namespace Common.Features.ResourcePoints
{
	public interface IResourceMandatoryPredicateFactory<TEntity,TKey>
		where TEntity : BaseEntity<TKey>
		where TKey:struct
	{
		ExpressionStarter<TEntity> GetMandatoryPredicates();
	}
}
