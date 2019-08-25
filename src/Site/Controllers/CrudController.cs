using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Remboard.Infrastructure.BaseControllers;

namespace Remboard.Controllers
{
    [GenericControllerNameConvention]
    public class CrudController<TEntity>: ControllerBase
       // where TEntity:BaseEntityGuidKey
    {
        public IActionResult Index()
        {
            return Content($"Hello from a generic {typeof(TEntity).Name} controller.");
        }
    }
}
