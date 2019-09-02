using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
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
    public class CrudController<TEntity, TEntityDto> :ControllerBase
        where TEntity:BaseEntityGuidKey
    {
        private readonly ICrudTypedControllerDescriptor<TEntity, TEntityDto> _descriptor;

        private readonly RemboardContext _context;

        private readonly ILogger<CrudController<TEntity, TEntityDto>> _logger;

        private readonly IAuthorizationService _authorizationService;

        private readonly IMapper _mapper;

        public CrudController(RemboardContext context, EntityControllerRegistry registry,ILogger<CrudController<TEntity, TEntityDto>> logger, IAuthorizationService authorizationService, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _authorizationService = authorizationService;
            _mapper = mapper;

            _descriptor = registry.GetTypedDescriptor<TEntity, TEntityDto>();

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<TEntityDto>> Get([FromRoute]Guid id)
        {
            var result = await _authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Read);

            if (!result.Succeeded)
            {
                return Forbid();
            }

            var predicate =  _descriptor.GetMandatoryPredicate();

            predicate.And(i => i.Id == id);
            var entity =  await _context.Set<TEntity>().AsExpandable().FirstOrDefaultAsync(predicate);

            if (entity == null)
            {
                return NotFound();
            }

            var entityDto = _mapper.Map<TEntityDto>(entity);

            return Ok(entityDto);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EntityResponse),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<TEntityDto>> Post([FromBody]TEntityDto entityDto)
        {
            
            var result = await _authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Create);

            if (!result.Succeeded)
            {
                return Forbid();
            }

            var entity = _mapper.Map<TEntity>(entityDto);

            _logger.LogInformation("Start add the new entity {entity} with id {id}", entity, entity.Id);

            await _descriptor.CorrectEntityAsync(entity,entityDto);
            var validationResult = await _descriptor.ValidateAsync(entityDto);

            if (validationResult == null)
            {
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();

                entityDto = _mapper.Map<TEntityDto>(entity);
                await _descriptor.CorrectEntityDtoAsync(entityDto, entity);

                return Ok(entityDto);
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
        public async Task<ActionResult<TEntityDto>> Put([FromRoute]Guid id,[FromBody]TEntityDto entityDto)
        {
            var result = await _authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Update);

            if (!result.Succeeded)
            {
                return Forbid();
            }

            var getByIdSpec = new GetByIdSpecification<TEntity>(id);
            var predicate = _descriptor.GetMandatoryPredicate();

            predicate.And(getByIdSpec.IsSatisfiedBy());
            var foundEntity = await _context.Set<TEntity>().AsExpandable().FirstOrDefaultAsync(predicate);

            if (foundEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(entityDto, foundEntity);
            await _descriptor.CorrectEntityAsync(foundEntity,entityDto);
            
            var validationResult = await _descriptor.ValidateAsync(entityDto);


            if (validationResult == null)
            {
                _context.Set<TEntity>().Update(foundEntity);
                await _context.SaveChangesAsync();

                entityDto = _mapper.Map<TEntityDto>(foundEntity);

                await _descriptor.CorrectEntityDtoAsync(entityDto,foundEntity);

                return Ok(entityDto);
            }

            var errorResponse = new EntityResponse
            {
                Message = "Failed to save entity",
            };

            errorResponse.ValidationErrors.AddRange(validationResult);
            return BadRequest(errorResponse);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete([FromRoute]Guid id, [FromServices]OnlyTenantEntitiesSpecification<TEntity> onlyTenantEntitiesSpecification)
        {
            var result = await _authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Delete);

            if (!result.Succeeded)
            {
                return Forbid();
            }

            var predicate = _descriptor.GetMandatoryPredicate();
            var getByIdSpec = new GetByIdSpecification<TEntity>(id);

            predicate.And(getByIdSpec.IsSatisfiedBy());

            var foundEntity = await _context.Set<TEntity>().AsExpandable().FirstOrDefaultAsync(predicate);

            if (foundEntity == null)
            {
                predicate = LinqKit.PredicateBuilder.New<TEntity>(true);

                predicate.And(getByIdSpec.IsSatisfiedBy());
                predicate.And(onlyTenantEntitiesSpecification.IsSatisfiedBy());

                foundEntity = await _context.Set<TEntity>().AsExpandable().FirstOrDefaultAsync(predicate);

                if (foundEntity!=null && foundEntity.IsDeleted)
                {
                    return Ok();
                }

                return NotFound();
            }

            foundEntity.IsDeleted = true;

            _context.Set<TEntity>().Update(foundEntity);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
