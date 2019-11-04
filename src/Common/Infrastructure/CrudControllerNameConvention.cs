using System;
using Common.Extensions;
using Common.Features.ResourcePoints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Common.Infrastructure
{
    // Used to set the controller name for routing purposes. Without this convention the
    // names would be like 'GenericController`1[Widget]' instead of 'Widget'.
    //
    // Conventions can be applied as attributes or added to MvcOptions.Conventions.
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CrudControllerNameConvention : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
	        var name = controller.ControllerType.GetGenericTypeDefinition().Name;
			if (name =="CrudController`3")
            {
				var entityType = controller.ControllerType.GenericTypeArguments[0];
				controller.ControllerName = entityType.Name;
            }else if (controller.ControllerType.InheritsOrImplements(typeof(ResourcePointBaseController<,,>)))
			{
				var entityType = controller.ControllerType.GetEntityTypeOrNull();
				controller.ControllerName = entityType.Name;
			}
            
        }
    }
}
