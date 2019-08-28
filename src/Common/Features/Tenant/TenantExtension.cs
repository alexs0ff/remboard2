using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Common.Features.BaseEntity;

namespace Common.Features.Tenant
{
    public static class TenantExtension
    {
        public static Expression<Func<TOther, bool>> OnlyForTenanted<TOther>(Guid tenantId)
        {
            var item = Expression.Parameter(typeof(TOther), "e");

            var prop = Expression.Property(item, nameof(ITenantedEntity.TenantId));

            var tenantConstant = Expression.Constant(tenantId);

            var equal = Expression.Equal(prop, tenantConstant);

            var lambda = Expression.Lambda<Func<TOther, bool>>(equal, item);

            return lambda;
        }

    }
}
