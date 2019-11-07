using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Common.Features.Auth;
using Entities;
using Common.Features.ErrorFlow;
using Common.Features.ResourcePoints.Crud;
using Common.Features.ResourcePoints.Crud.Messaging;
using Common.Features.ResourcePoints.Filterable;
using Common.Features.ResourcePoints.Schema;
using Common.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Common.Features.ResourcePoints
{
	[CrudControllerNameConvention]
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class CrudResourcePointController<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey>: ResourcePointBaseController<TEntity, TFilterableEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TKey : struct
		where TCreateEntityDto : class
		where TEditEntityDto : class
	{
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<ActionResult<TEditEntityDto>> Get([FromRoute]string id, [FromServices] RemboardContext context, [FromServices] IAuthorizationService authorizationService, [FromServices] ILogger<CrudResourcePointController<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey>> logger, [FromServices] CrudResourcePointControllerFactory<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey> controllerFactory)
		{
			var result = await authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Read);

			if (!result.Succeeded)
			{
				return Forbid();
			}

			var operation = controllerFactory.GetCrudOperation();

			try
			{
				var entityDto = await operation.Get(id, context, controllerFactory.GetMandatoryPredicateFactory());

				if (entityDto == null)
				{
					var entityName = typeof(TEntity).Name;
					logger.LogError("The entity '{entityName}' with {id} doesn`t exists ", id, entityName);
					return NotFound();
				}

				return Ok(entityDto);
			}
			catch (WrongIdValueException)
			{
				var entityName = typeof(TEntity).Name;
				logger.LogError("Can`t parse {id} for {entityName}", id, entityName);
				return NotFound();
			}
		}

		[HttpPost()]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(EntityResponse), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<ActionResult<TEditEntityDto>> Post([FromBody] TCreateEntityDto entityDto, [FromServices] RemboardContext context, [FromServices] IAuthorizationService authorizationService, [FromServices] CrudResourcePointControllerFactory<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey> controllerFactory)
		{
			var result = await authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Create);

			if (!result.Succeeded)
			{
				return Forbid();
			}

			var validator = controllerFactory.GetCreateDtoValidator();

			var validationResult = await ValidateAsync(validator,entityDto);

			if (validationResult == null)
			{
				var operation = controllerFactory.GetCrudOperation();
				var correctors = controllerFactory.GetCorrectors();
				var saved = await operation.Post(entityDto, context, correctors);

				var producers = controllerFactory.GetAfterEntityCreateCommandProducersOrNull();

				await producers.SendToAll(entityDto, saved.entity.Id);

				return Ok(saved.entityDto);
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
		public async Task<ActionResult<TEditEntityDto>> Put([FromRoute] string id, [FromBody] TEditEntityDto entityDto, [FromServices] RemboardContext context, [FromServices] IAuthorizationService authorizationService, [FromServices] CrudResourcePointControllerFactory<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey> controllerFactory, [FromServices] ILogger<CrudResourcePointController<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey>> logger)
		{
			var result = await authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Create);

			if (!result.Succeeded)
			{
				return Forbid();
			}

			var validator = controllerFactory.GetEditDtoValidator();

			var validationResult = await ValidateAsync(validator, entityDto);

			if (validationResult == null)
			{
				var predicates = controllerFactory.GetMandatoryPredicateFactory();
				var operation = controllerFactory.GetCrudOperation();
				var correctors = controllerFactory.GetCorrectors();
				try
				{
					var saved = await operation.Put(id, entityDto, context, predicates, correctors);

					var producers = controllerFactory.GetAfterEntityEditCommandProducersOrNull();
					await producers.SendToAll(entityDto, saved.entity.Id);

					return Ok(saved.entityDto);
				}
				catch (WrongIdValueException)
				{
					var entityName = typeof(TEntity).Name;
					logger.LogError("Can`t parse {id} for {entityName}", id, entityName);
					return NotFound();
				}
				catch (EntityNotFoundException)
				{
					var entityName = typeof(TEntity).Name;
					logger.LogError("The entity '{entityName}' with {id} doesn`t exists ", id, entityName);
					return NotFound();
				}
				
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
		public async Task<IActionResult> Delete([FromRoute] string id, [FromServices] RemboardContext context, [FromServices] IAuthorizationService authorizationService, [FromServices] CrudResourcePointControllerFactory<TEntity, TCreateEntityDto,TEditEntityDto, TFilterableEntity, TKey> controllerFactory, [FromServices] ILogger<CrudResourcePointController<TEntity, TCreateEntityDto,TEditEntityDto, TFilterableEntity, TKey>> logger)
		{
			var result = await authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Delete);

			if (!result.Succeeded)
			{
				return Forbid();
			}

			try
			{
				var operation = controllerFactory.GetCrudOperation();
				var predicates = controllerFactory.GetMandatoryPredicateFactory();
				await operation.Delete(id, context, predicates);
			}
			catch (WrongIdValueException)
			{
				var entityName = typeof(TEntity).Name;
				logger.LogError("Can`t parse {id} for {entityName}", id, entityName);
				return NotFound();
			}
			catch (EntityNotFoundException)
			{
				var entityName = typeof(TEntity).Name;
				logger.LogError("The entity '{entityName}' with {id} doesn`t exists ", id, entityName);
				return NotFound();
			}

			return Ok();
		}

		[HttpGet("/api/[controller]/createSchema")]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<EntityEditFormModel>> GetCreateSchema([FromServices] IAuthorizationService authorizationService, [FromServices] CrudResourcePointControllerFactory<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey> controllerFactory)
		{
			var createResult = await authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Create);

			if (!createResult.Succeeded)
			{
				return Forbid();
			}

			var schemaProvider = controllerFactory.GetEntityFormSchemaProvider();
			var context = new EntityEditSchemaProviderContext();
			
			var model = await schemaProvider.GetCreateModelAsync(context);
			return Ok(model);
		}

		[HttpGet("/api/[controller]/editSchema")]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<EntityEditFormModel>> GetEditSchema([FromServices] IAuthorizationService authorizationService, [FromServices] CrudResourcePointControllerFactory<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey> controllerFactory)
		{
			var updateResult = await authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Update);

			if (!updateResult.Succeeded)
			{
				return Forbid();
			}

			var schemaProvider = controllerFactory.GetEntityFormSchemaProvider();
			var context = new EntityEditSchemaProviderContext();
			
			var model = await schemaProvider.GetEditModelAsync(context);
			return Ok(model);
		}

		private async Task<ValidationErrorItem[]> ValidateAsync<TEntityDto>(IValidator<TEntityDto> validator, TEntityDto entityDto)
		where TEntityDto:class
		{
			var result = await validator.ValidateAsync(entityDto);

			if (result.IsValid)
			{
				return null;
			}

			return result.Errors.Select(i => new ValidationErrorItem
				{ Property = i.PropertyName, Message = i.ErrorMessage }).ToArray();
		}
	}
}
