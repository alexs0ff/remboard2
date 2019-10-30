using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Entities;
using Common.Features.Specifications;

namespace Common.Features.BaseEntity
{
	public class IsNotDeletedSpecification<TEntity,TKey> : ISpecification<TEntity>
		where TEntity : BaseEntity<TKey>
		where TKey:struct
	{
		public Expression<Func<TEntity, bool>> IsSatisfiedBy()
		{
			return e => e.IsDeleted == false;
		}
	}
}
