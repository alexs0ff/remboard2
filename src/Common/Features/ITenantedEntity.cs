using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features
{
    public interface ITenantedEntity
    {
        Guid TenantId { get; set; }
    }
}
