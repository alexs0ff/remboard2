using System;
using Common.FeatureEntities;

namespace Common.Features.Users
{
    public interface ICurrentIdentityInfoProvider
    {
        Guid? GetTenantId();
        ProjectRoles[] GetRoles();
    }
}
