using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Data;
using Common.Features;
using Common.Features.Cruds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Logging;
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
