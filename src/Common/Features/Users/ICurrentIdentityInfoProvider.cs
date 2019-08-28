using System;

namespace Common.Features.Users
{
    public interface ICurrentIdentityInfoProvider
    {
        Guid? GetTenantId();
    }
}
