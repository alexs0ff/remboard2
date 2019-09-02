﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Common.Features.Specifications;

namespace Common.Features.BaseEntity
{
    public class GetByIdSpecification<TEntity>: ISpecification<TEntity>
        where TEntity : BaseEntityGuidKey
    {
        private readonly Guid _id;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public GetByIdSpecification(Guid id)
        {
            _id = id;
        }

        public Expression<Func<TEntity, bool>> IsSatisfiedBy()
        {
            return e => e.Id == _id;
        }
    }
}
