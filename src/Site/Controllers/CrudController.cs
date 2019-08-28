using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Data;
using Common.Features;
using Common.Features.BaseEntity;
using Common.Features.Cruds;
using Common.Features.Tenant;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Remboard.Infrastructure.BaseControllers;

namespace Remboard.Controllers
{
    [GenericControllerNameConvention]
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class CrudController<TEntity>:ControllerBase
        where TEntity:BaseEntityGuidKey
    {
        private readonly ICrudControllerDescriptor _descriptor;

        private readonly RemboardContext _context;

        private readonly ILogger<CrudController<TEntity>> _logger;

        public CrudController(RemboardContext context, EntityControllerRegistry registry,ILogger<CrudController<TEntity>> logger)
        {
            _context = context;
            _logger = logger;

            _descriptor = registry[typeof(TEntity).Name];

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TEntity>> Get([FromRoute]string id)
        {
            var tenantId = Guid.Parse("A31CCB6A-8B28-4B96-A278-E3CF9DF5E130");
            var predicate =  LinqKit.PredicateBuilder.New<TEntity>(true);
            
            predicate.And(BaseEntityExtensions.IsNotDeleted<TEntity>());
            predicate.And(BaseEntityExtensions.IsNot2<TEntity>());
            predicate.And(TenantExtension.OnlyForTenanted<TEntity>(tenantId));
           
            var list =await _context.Set<TEntity>().AsExpandable().Where(predicate).ToArrayAsync();
            //var list = _context.Set<TEntity>().Where(predicate.Compile()).ToArray();

            var c = list.Length;

            //ВОт библиотека https://github.com/scottksmith95/LINQKit
            //https://habr.com/ru/post/335856/
            //http://www.albahari.com/nutshell/linqkit.aspx
            //Делать биндинг на параметр tenantId в JWT токене
            //https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/parameter-binding-in-aspnet-web-api#httpparameterbinding
            return Ok(new {Awwe=222});
            //return NotFound();
        }
    }
}
