using System;

namespace Common.Features.Tenant
{
    public interface ITenantedEntity
    {
        Guid TenantId { get; set; }
    }
}
