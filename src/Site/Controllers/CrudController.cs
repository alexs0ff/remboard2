using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Data;
using Common.Features;
using Common.Features.BaseEntity;
using Common.Features.Cruds;
using Common.Features.ErrorFlow;
using Common.Features.Tenant;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Remboard.Auth.Roles;
using Remboard.Infrastructure.BaseControllers;

namespace Remboard.Controllers
{
    [GenericControllerNameConvention]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CrudController<TEntity>:ControllerBase
        where TEntity:BaseEntityGuidKey
    {
        private readonly ICrudTypedControllerDescriptor<TEntity> _descriptor;

        private readonly RemboardContext _context;

        private readonly ILogger<CrudController<TEntity>> _logger;

        private readonly IAuthorizationService _authorizationService;

        public CrudController(RemboardContext context, EntityControllerRegistry registry,ILogger<CrudController<TEntity>> logger, IAuthorizationService authorizationService)
        {
            _context = context;
            _logger = logger;
            _authorizationService = authorizationService;

            _descriptor = registry.GetTypedDescriptor<TEntity>();

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<TEntity>> Get([FromRoute]Guid id)
        {
            var result = await _authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Read);

            if (!result.Succeeded)
            {
                return Forbid();
            }

            var predicate =  _descriptor.GetMandatoryPredicate();

            predicate.And(i => i.Id == id);
            var res =  await _context.Set<TEntity>().AsExpandable().FirstOrDefaultAsync(predicate);

            if (res == null)
            {
                return NotFound();
            }
            
            return Ok(res);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EntityResponse),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<TEntity>> Post([FromBody]TEntity entity)
        {
            _logger.LogInformation("Start add the new entity {entity} with id {id}",entity,entity.Id);
            var result = await _authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Create);

            if (!result.Succeeded)
            {
                return Forbid();
            }

            await _descriptor.CorrectBeforeAsync(entity);

            var validationResult = await _descriptor.ValidateAsync(entity);

            if (validationResult == null)
            {
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();
                await _descriptor.CorrectAfterAsync(entity);

                return Ok(entity);
            }

            var errorResponse = new EntityResponse
            {
                Message = "Failed to save entity",
            };

            errorResponse.ValidationErrors.AddRange(validationResult);
            return BadRequest(errorResponse);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(EntityResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TEntity>> Put([FromRoute]Guid id,[FromBody]TEntity entity)
        {
            var result = await _authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Update);

            if (!result.Succeeded)
            {
                return Forbid();
            }

            var predicate = _descriptor.GetMandatoryPredicate();

            predicate.And(i => i.Id == id);
            var res = await _context.Set<TEntity>().AsExpandable().FirstOrDefaultAsync(predicate);

            if (res == null)
            {
                return NotFound();
            }


            await _descriptor.CorrectBeforeAsync(entity);

            var validationResult = await _descriptor.ValidateAsync(entity);

            if (validationResult == null)
            {
                //_context.Set<TEntity>().Add(entity);
                //await _context.SaveChangesAsync();
                TODO: Mapper лучше сделать через Descriptor и переопределять маппер по умолчанию (который не копирует свойства тип ID timestamp И тд)

                await _descriptor.CorrectAfterAsync(entity);

                return Ok(entity);
            }

            var errorResponse = new EntityResponse
            {
                Message = "Failed to save entity",
            };

            errorResponse.ValidationErrors.AddRange(validationResult);
            return BadRequest(errorResponse);
        }
    }
}
