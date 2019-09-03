using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Features.Cruds;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Remboard.Controllers;

namespace Remboard.Infrastructure.BaseControllers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PluralActionNameConventionAttribute : Attribute
    {

    }

    public class PluralActionNameConvention : IActionModelConvention
    {
        private readonly EntityControllerRegistry _controllerRegistry;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public PluralActionNameConvention(EntityControllerRegistry controllerRegistry)
        {
            _controllerRegistry = controllerRegistry;
        }

        /// <summary>
        /// Called to apply the convention to the <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ActionModel" />.
        /// </summary>
        /// <param name="action">The <see cref="T:Microsoft.AspNetCore.Mvc.ApplicationModels.ActionModel" />.</param>
        public void Apply(ActionModel action)
        {
            if (action.Controller.ControllerType.IsGenericType && action.Controller.ControllerType.GetGenericTypeDefinition() ==
                typeof(CrudController<,,>))
            {
                var hasAttribute = action.Attributes.Cast<Attribute>().Any(a => a.GetType() == typeof(PluralActionNameConventionAttribute));
                if (hasAttribute)
                {
                    var entityType = action.Controller.ControllerType.GenericTypeArguments[0];
                    action.ActionName = _controllerRegistry[entityType.Name].EntityDescriptor.EntityPluralName;
                }
                
            }

        }
    }
}
