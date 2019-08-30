using System;
using Common.Extensions;
using Common.Features.BaseEntity;
using Common.Features.Tenant;
using FluentValidation;

namespace Common.Features
{
    public abstract class BaseValidator<TEntity,TKey> : AbstractValidator<TEntity>
        where TEntity:BaseEntity<TKey>
        where TKey : struct
    {
        protected BaseValidator()
        {
            RuleFor(i => i.IsDeleted).Equal(false);

            if (typeof(TEntity).HasImplementation<ITenantedEntity>())
            {
                RuleFor(e => e).Must(entity => (((ITenantedEntity) entity).TenantId != Guid.Empty)).OverridePropertyName("TenantId").WithMessage("TenantId is not set");
            }
        }
    }
}
