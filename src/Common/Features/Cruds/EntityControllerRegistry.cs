using System.Collections.Generic;

namespace Common.Features.Cruds
{
    public class EntityControllerRegistry
    {
        private readonly IEnumerable<ICrudControllerDescriptor> _crudControllerDescriptors;

        public EntityControllerRegistry(IEnumerable<ICrudControllerDescriptor> crudControllerDescriptors)
        {
            _crudControllerDescriptors = crudControllerDescriptors;
        }

        public IEnumerable<ICrudControllerDescriptor> CrudControllerDescriptors => _crudControllerDescriptors;
    }
}
