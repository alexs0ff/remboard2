using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Features.BaseEntity;

namespace Common.Features.Tenant
{
	[Obsolete]
    public class TenantedEntityCorrectorGuid:IEntityCorrectorGuid<BaseEntityGuidKey, object>
    {
        private readonly ITenantInfoProvider _tenantInfoProvider;

        public TenantedEntityCorrectorGuid(ITenantInfoProvider tenantInfoProvider)
        {
            _tenantInfoProvider = tenantInfoProvider;
        }

        public Task CorrectEntityAsync(BaseEntityGuidKey entity, object receivedEntityDto)
        {
            ((ITenantedEntity)entity).TenantId = _tenantInfoProvider.GetCurrentTenantId() ?? Guid.Empty;
            return Task.CompletedTask;
        }

        public Task CorrectEntityDtoAsync(object entityDto, BaseEntityGuidKey entity)
        {
            return Task.CompletedTask;
        }
    }
}
