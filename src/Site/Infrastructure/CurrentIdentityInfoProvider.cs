using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.FeatureEntities;
using Common.Features.Users;
using Microsoft.AspNetCore.Http;
using Remboard.Auth;
using Users.Api;

namespace Remboard.Infrastructure
{
    public class CurrentIdentityInfoProvider: ICurrentIdentityInfoProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentIdentityInfoProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? GetTenantId()
        {
            var claims = _httpContextAccessor.HttpContext.User?.Claims;

            if (claims == null)
            {
                return null;
            }

            var claimValue = claims.FirstOrDefault(c => StringComparer.Ordinal.Equals(c.Type, RemboardClaims.Tenant))?.Value;

            return Guid.TryParse(claimValue, out var result) ? result:(Guid?)null;
        }

        public ProjectRoles[] GetRoles()
        {
            var claims = _httpContextAccessor.HttpContext.User?.Claims;

            if (claims == null)
            {
                return null;
            }

            return claims.Where(c => StringComparer.Ordinal.Equals(c.Type, ClaimsIdentity.DefaultRoleClaimType)).Select(i=>Enum.Parse<ProjectRoles>(i.Value)).ToArray();
        }
    }
}
