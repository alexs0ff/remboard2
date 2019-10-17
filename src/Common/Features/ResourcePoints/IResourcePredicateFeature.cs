using System;
using System.Collections.Generic;
using System.Text;
using LinqKit;

namespace Common.Features.ResourcePoints
{
	public interface IResourcePredicateFeature<TEntity,TKey>
		where TEntity : Common.Features.BaseEntity.BaseEntity<TKey>
		where TKey:struct
	{
		ExpressionStarter<TEntity> GetMandatoryPredicate();
	}
}
