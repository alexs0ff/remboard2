using System;
using Common.Extensions;
using Common.Features.BaseEntity;
using Entities;

namespace Common.Features
{
    public abstract class BaseEntityGuidKeyValidator<TEntity>: BaseValidator<TEntity,Guid>
        where TEntity: BaseEntityGuidKey
    {
        protected BaseEntityGuidKeyValidator()
        {
            RuleFor(i => i.Id).GuidIsNotEmpty<TEntity,Guid>();
        }
    }
}
