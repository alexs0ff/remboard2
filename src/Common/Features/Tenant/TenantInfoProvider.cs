using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.Users;

namespace Common.Features.Tenant
{
    public class TenantInfoProvider: ITenantInfoProvider
    {
        private readonly ICurrentIdentityInfoProvider _currentIdentityInfoProvider;

        public TenantInfoProvider(ICurrentIdentityInfoProvider currentIdentityInfoProvider)
        {
            _currentIdentityInfoProvider = currentIdentityInfoProvider;
        }

        public Guid? GetCurrentTenantId()
        {
            return _currentIdentityInfoProvider.GetTenantId();
        }
    }
}
