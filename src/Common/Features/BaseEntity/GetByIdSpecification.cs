using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Entities;
using Common.Features.Specifications;

namespace Common.Features.BaseEntity
{
	public class GetByIdSpecification<TEntity,TKey> : ISpecification<TEntity>
		where TEntity : BaseEntity<TKey>
		where TKey:struct
	{
		private readonly TKey _id;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public GetByIdSpecification(TKey id)
		{
			_id = id;
		}

		public Expression<Func<TEntity, bool>> IsSatisfiedBy()
		{
			return e => e.Id.Equals(_id);
		}
	}
}
