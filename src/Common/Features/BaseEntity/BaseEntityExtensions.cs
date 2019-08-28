using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Common.Features.Tenant;

namespace Common.Features.BaseEntity
{
    public static class BaseEntityExtensions
    {
        public static Expression<Func<TEntity, bool>> IsNotDeleted<TEntity>()
            where TEntity: BaseEntityGuidKey
        {
            return e => e.IsDeleted == false;
        }

        public static Expression<Func<TEntity, bool>> IsNot2<TEntity>()
            where TEntity : BaseEntityGuidKey
        {
            return e => e.Id != Guid.Empty;
        }

        public static Expression<Func<TEntity, bool>> OnlyForTenant<TEntity>(Guid tenantId)
            where TEntity : BaseEntityGuidKey
        {
            return TenantExtension.OnlyForTenanted<TEntity>(tenantId);
        }
    }
}
