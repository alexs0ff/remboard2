using System;
using Common.FeatureEntities;
using Entities;

namespace Common.Features.Users
{
    public interface ICurrentIdentityInfoProvider
    {
        Guid? GetTenantId();
        ProjectRoles[] GetRoles();
    }
}
