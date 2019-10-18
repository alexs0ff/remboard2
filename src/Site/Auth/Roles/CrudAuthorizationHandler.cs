using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Common.Features;
using Common.Features.Auth;
using Common.Features.PermissibleValues;
using Common.Features.ResourcePoints;
using Common.Features.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Users;

namespace Remboard.Auth.Roles
{
    public class CrudAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, Type>
    {

        private readonly PermissibleValuesControllerRegistry _permissibleValuesControllerRegistry;

        private readonly ResourcePointControllerRegistry _resourcePointControllerRegistry;

        private readonly ICurrentIdentityInfoProvider _currentIdentityInfoProvider;

        public CrudAuthorizationHandler(ICurrentIdentityInfoProvider currentIdentityInfoProvider, PermissibleValuesControllerRegistry permissibleValuesControllerRegistry, ResourcePointControllerRegistry resourcePointControllerRegistry)
        {
            _currentIdentityInfoProvider = currentIdentityInfoProvider;
            _permissibleValuesControllerRegistry = permissibleValuesControllerRegistry;
            _resourcePointControllerRegistry = resourcePointControllerRegistry;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Type resource)
        {
            AccessRuleMap accessRules = null;

            if (_resourcePointControllerRegistry.HasEntity(resource.Name))
            {
	            accessRules = _resourcePointControllerRegistry[resource.Name].AccessRules;
            }else

            if (_permissibleValuesControllerRegistry.HasEntity(resource.Name))
            {
	            accessRules = _permissibleValuesControllerRegistry[resource.Name].AccessRules;
			}


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
