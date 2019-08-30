using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Features.BaseEntity;

namespace Common.Features.Tenant
{
    public class TenantedEntityCorrector:IEntityCorrector<BaseEntityGuidKey>
    {
        private readonly ITenantInfoProvider _tenantInfoProvider;

        public TenantedEntityCorrector(ITenantInfoProvider tenantInfoProvider)
        {
            _tenantInfoProvider = tenantInfoProvider;
        }

        public Task CorrectBefore(BaseEntityGuidKey entity)
        {
            ((ITenantedEntity) entity).TenantId = _tenantInfoProvider.GetCurrentTenantId()??Guid.Empty;
            return Task.CompletedTask;
        }

        public Task CorrectAfter(BaseEntityGuidKey entity)
        {
            return Task.CompletedTask;
        }
    }
}
