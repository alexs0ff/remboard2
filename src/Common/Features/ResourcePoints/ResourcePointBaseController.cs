using System;
using System.Collections.Generic;
using Entities;
using System.Threading.Tasks;
using AutoMapper;
using Common.Data;
using Common.Features.Auth;
using Common.Features.BaseEntity;
using Common.Features.ResourcePoints.Crud;
using Common.Features.ResourcePoints.Filterable;
using Common.Features.ResourcePoints.Filterable.Schema;
using Common.Infrastructure;
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
	public class ResourcePointBaseController<TEntity, TFilterableEntity, TKey> : ControllerBase
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TKey:struct
	{
		public ResourcePointBaseController(/*!all dependencies should get [FromServices] attribute*/)
		{
		}

		[PluralActionNameConvention]
		[HttpGet("/api/[action]")]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<PagedResult<TFilterableEntity>>> Get(FilterParameters filterParameters,[FromServices] RemboardContext context, [FromServices] IAuthorizationService authorizationService, [FromServices] ResourcePointControllerFactory<TEntity, TFilterableEntity, TKey> controllerFactory)
		{
			var result = await authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Read);

			if (!result.Succeeded)
			{
				return Forbid();
			}

			var filterableOperation = controllerFactory.GetFilterableOperation();
			var predicateFactory = controllerFactory.GetMandatoryPredicateFactory();
			var pagedResult = await filterableOperation.FilterAsync(context, predicateFactory, filterParameters);
			return Ok(pagedResult);
		}

		[HttpGet("/api/[controller]/gridSchema")]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<ServerDataGridModel>> GridSchema([FromServices] IAuthorizationService authorizationService, [FromServices] ResourcePointControllerFactory<TEntity, TFilterableEntity, TKey> controllerFactory)
		{
			var result = await authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Read);

			if (!result.Succeeded)
			{
				return Forbid();
			}

			var schemaProvider = controllerFactory.GetEntitySchemaProvider();

			var model = await schemaProvider.GetModelAsync(new EntitySchemaProviderContext());
			return Ok(model);
		}
	}
}
