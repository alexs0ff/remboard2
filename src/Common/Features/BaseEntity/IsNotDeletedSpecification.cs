using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Common.Features.Specifications;

namespace Common.Features.BaseEntity
{
    public class IsNotDeletedSpecification<TEntity> : ISpecification<TEntity>
        where TEntity : BaseEntityGuidKey
    {
        public Expression<Func<TEntity, bool>> IsSatisfiedBy()
        {
            return e => e.IsDeleted == false;
        }
    }
}
