using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Data;
using Common.Features.Auth;
using Common.Features.BaseEntity;
using Common.Features.ResourcePoints.Filterable;
using Common.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.Features.ResourcePoints
{
	[CrudControllerNameConvention]
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class ResourcePointBaseController<TEntity, TEntityDto, TFilterableEntity, TKey> : ControllerBase
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TKey:struct
		where TEntityDto :class
	{
		public ResourcePointBaseController(/*!all dependencies should get [FromServices] attribute*/)
		{
		}

		[PluralActionNameConvention]
		[HttpGet("/api/[action]")]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<ActionResult<PagedResult<TFilterableEntity>>> Get(FilterParameters filterParameters,[FromServices] RemboardContext context, [FromServices] IAuthorizationService authorizationService, [FromServices] ResourcePointControllerFactory<TEntity, TEntityDto, TFilterableEntity, TKey> controllerFactory)
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
	}
}
