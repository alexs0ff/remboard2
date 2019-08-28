using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Common.Features.BaseEntity;
using Common.Features.Specifications;

namespace Common.Features.Tenant
{
    public class OnlyTenantEntitiesSpecification<TEntity> : ISpecification<TEntity>
        where TEntity : BaseEntityGuidKey
    {
        private readonly ITenantInfoProvider _tenantInfoProvider;

        public OnlyTenantEntitiesSpecification(ITenantInfoProvider tenantInfoProvider)
        {
            _tenantInfoProvider = tenantInfoProvider;
        }

        public Expression<Func<TEntity, bool>> IsSatisfiedBy()
        {
            Guid? tenantId = _tenantInfoProvider.GetCurrentTenantId();

            if (tenantId == null)
            {
                throw new Exception("Cannot get the current tenant id");
            }

            var item = Expression.Parameter(typeof(TEntity), "e");

            var prop = Expression.Property(item, nameof(ITenantedEntity.TenantId));

            var tenantConstant = Expression.Constant(tenantId.Value);

            var equal = Expression.Equal(prop, tenantConstant);

            var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, item);

            return lambda;
        }
    }
}
