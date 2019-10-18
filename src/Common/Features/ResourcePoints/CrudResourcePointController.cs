using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Common.Features.Auth;
using Common.Features.BaseEntity;
using Common.Features.ErrorFlow;
using Common.Features.ResourcePoints.Crud;
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
	public class CrudResourcePointController<TEntity, TEntityDto, TFilterableEntity, TKey>: ResourcePointBaseController<TEntity, TEntityDto, TFilterableEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TKey : struct
		where TEntityDto : class
	{
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<ActionResult<TEntityDto>> Get([FromRoute]string id, [FromServices] RemboardContext context, [FromServices] IAuthorizationService authorizationService, [FromServices] ILogger<ResourcePointBaseController<TEntity, TEntityDto, TFilterableEntity, TKey>> logger, [FromServices] CrudResourcePointControllerFactory<TEntity, TEntityDto, TFilterableEntity, TKey> controllerFactory)
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
		public async Task<ActionResult<TEntityDto>> Post([FromBody] TEntityDto entityDto, [FromServices] RemboardContext context, [FromServices] IAuthorizationService authorizationService, [FromServices] CrudResourcePointControllerFactory<TEntity, TEntityDto, TFilterableEntity, TKey> controllerFactory)
		{
			var result = await authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Create);

			if (!result.Succeeded)
			{
				return Forbid();
			}

			var validator = controllerFactory.GetValidator();

			var validationResult = await ValidateAsync(validator,entityDto);

			if (validationResult == null)
			{
				var operation = controllerFactory.GetCrudOperation();
				var correctors = controllerFactory.GetCorrectors();
				var saved = await operation.Post(entityDto, context, correctors);

				return Ok(saved);
			}

			var errorResponse = new EntityResponse
			{
				Message = "Failed to save entity",
			};

			errorResponse.ValidationErrors.AddRange(validationResult);
			return BadRequest(errorResponse);
		}


		private async Task<ValidationErrorItem[]> ValidateAsync(IValidator<TEntityDto> validator, TEntityDto entityDto)
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
