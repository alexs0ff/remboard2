using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Common.Features.Auth;
using Common.Features.Cruds;
using Common.Features.PermissibleValues;
using Common.Features.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Users;

namespace Remboard.Auth.Roles
{
    public class CrudAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, Type>
    {
        private readonly EntityControllerRegistry _entityControllerRegistry;

        private readonly PermissibleValuesControllerRegistry _permissibleValuesControllerRegistry;

        private readonly ICurrentIdentityInfoProvider _currentIdentityInfoProvider;

        public CrudAuthorizationHandler(EntityControllerRegistry entityControllerRegistry, ICurrentIdentityInfoProvider currentIdentityInfoProvider, PermissibleValuesControllerRegistry permissibleValuesControllerRegistry)
        {
            _entityControllerRegistry = entityControllerRegistry;
            _currentIdentityInfoProvider = currentIdentityInfoProvider;
            _permissibleValuesControllerRegistry = permissibleValuesControllerRegistry;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Type resource)
        {
            AccessRuleMap accessRules;

            if (_entityControllerRegistry.HasEntity(resource.Name)==false)
            {
                accessRules = _permissibleValuesControllerRegistry[resource.Name].AccessRules;
            }
            else
            {
                accessRules = _entityControllerRegistry[resource.Name].AccessRules;
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
