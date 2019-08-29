using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Common.Features.Cruds;
using Common.Features.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Users;

namespace Remboard.Auth.Roles
{
    public class CrudAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, Type>
    {
        private readonly EntityControllerRegistry _registry;

        private readonly ICurrentIdentityInfoProvider _currentIdentityInfoProvider;

        public CrudAuthorizationHandler(EntityControllerRegistry registry, ICurrentIdentityInfoProvider currentIdentityInfoProvider)
        {
            _registry = registry;
            _currentIdentityInfoProvider = currentIdentityInfoProvider;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Type resource)
        {
            var accessRules = _registry[resource.Name].AccessRules;

            var roles = _currentIdentityInfoProvider.GetRoles();
            if (requirement.Name == CrudOperations.Read.Name)
            {
                if (accessRules.CanRead(roles))
                {
                    context.Succeed(requirement);
                }
            }
            else
            {
                if (accessRules.CanModify(roles))
                {
                    context.Succeed(requirement);
                }
            }
            

            return Task.CompletedTask;
        }
    }
}
