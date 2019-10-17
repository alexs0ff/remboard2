﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
		private readonly ResourcePointControllerFactory<TEntity, TEntityDto, TFilterableEntity, TKey> _controllerFactory;

		private readonly IAuthorizationService _authorizationService;

		private readonly IMapper _mapper;

		public ResourcePointBaseController(ResourcePointControllerFactory<TEntity, TEntityDto, TFilterableEntity, TKey> controllerFactory, IAuthorizationService authorizationService, IMapper mapper)
		{
			_controllerFactory = controllerFactory;
			_authorizationService = authorizationService;
			_mapper = mapper;
		}

		[PluralActionNameConvention]
		[HttpGet("/api/[action]")]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<ActionResult<PagedResult<TFilterableEntity>>> Get(/*FilterParameters filterParameters*/)
		{
			/*var result = await _authorizationService.AuthorizeAsync(User, typeof(TEntity), CrudOperations.Read);

			if (!result.Succeeded)
			{
				return Forbid();
			}*/

			/*var pagedResult = await _filterableOperation.GetFiltarableOperation().FilterAsync(_context,_descriptor, filterParameters);

			return Ok(pagedResult);*/
			return Ok("successs");
		}
	}
}
