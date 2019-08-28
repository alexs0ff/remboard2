using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.Tenant
{
    public interface ITenantInfoProvider
    {
        /// <summary>
        /// The id of current tenant or null.
        /// </summary>
        /// <returns></returns>
        Guid? GetCurrentTenantId();
    }
}
