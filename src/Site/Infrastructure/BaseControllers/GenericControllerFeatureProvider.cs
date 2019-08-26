using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Common.Features.Cruds;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Remboard.Controllers;

namespace Remboard.Infrastructure.BaseControllers
{
    public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly EntityControllerRegistry _controllerRegistry;

        public GenericControllerFeatureProvider(EntityControllerRegistry controllerRegistry)
        {
            _controllerRegistry = controllerRegistry;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            // This is designed to run after the default ControllerTypeProvider, 
            // so the list of 'real' controllers has already been populated.
            foreach (var crudControllerDescriptor in _controllerRegistry.CrudControllerDescriptors)
            {
                var typeName = crudControllerDescriptor.EntityName + "Controller";
                if (!feature.Controllers.Any(t => t.Name == typeName))
                {
                    // There's no 'real' controller for this entity, so add the generic version.
                    var controllerType = typeof(CrudController<>)
                        .MakeGenericType(crudControllerDescriptor.EntityDescriptor.TypeInfo.AsType()).GetTypeInfo();
                    feature.Controllers.Add(controllerType);
                }
            }
        }
    }

    public static class EntityTypes
    {
        public static IReadOnlyList<TypeInfo> Types => new List<TypeInfo>()
        {
            typeof(Sprocket).GetTypeInfo(),
            typeof(Widget).GetTypeInfo(),
        };

        public class Sprocket { }
        public class Widget { }
    }
}
