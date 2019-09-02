using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Features.Cruds;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Remboard.Controllers;

namespace Remboard.Infrastructure.BaseControllers
{
    public class PluralActionNameConvention : IActionModelConvention
    {
        public EntityControllerRegistry Registry { get; set; }
        /// <summary>
        /// Called to apply the convention to the <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ActionModel" />.
        /// </summary>
        /// <param name="action">The <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ActionModel" />.</param>
        public void Apply(ActionModel action)
        {
            if (action.Controller.ControllerType.GetGenericTypeDefinition() !=
                typeof(CrudController<,>))
            {
                // Not a GenericController, ignore.
                return;
            }

            var entityType = action.Controller.ControllerType.GenericTypeArguments[0];

            

        }
    }
}
