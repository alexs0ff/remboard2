using System;

namespace Entities
{
    public interface ITenantedEntity
    {
        Guid TenantId { get; set; }
    }
}
