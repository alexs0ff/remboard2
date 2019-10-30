using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Features;
using Common.Features.Auth;
using Common.Features.PermissibleValues;
using Common.Infrastructure;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Remboard.Auth.Roles;
using Remboard.Infrastructure.BaseControllers;

namespace Remboard.Controllers
{
    //[CrudControllerNameConvention]
    [ApiController]
    [Route("api/[controller]")]
    public class PermissibleValuesController<TEntity,TEnum>: ControllerBase
        where TEntity : BasePermissibleValue<TEnum>
        where TEnum : Enum
    {
        private readonly ILogger<PermissibleValuesController<TEntity, TEnum>> _logger;

        private readonly IAuthorizationService _authorizationService;

        private readonly IPermissibleValuesTypedControllerDescriptor<TEntity, TEnum> _descriptor;

        public PermissibleValuesController(PermissibleValuesControllerRegistry registry,ILogger<PermissibleValuesController<TEntity, TEnum>> logger, IAuthorizationService authorizationService)
        {
            _logger = logger;
            _authorizationService = authorizationService;
            _descriptor = registry.GetTypedDescriptor<TEntity, TEnum>();
        }

        [PluralActionNameConvention]
        [HttpGet("/api/[action]")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            var result = await _authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Read);

            if (!result.Succeeded)
            {
                return Forbid();
            }

            var entities = await _descriptor.PermissibleValuesProvider.ReadEntitiesAsync();

            return Ok(entities);
        }
    }
}
