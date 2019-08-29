using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Common.Features.BaseEntity;

namespace Common.Features.Cruds
{
    public class EntityControllerRegistry
    {
        private readonly IReadOnlyDictionary<string,ICrudControllerDescriptor> _crudControllerDescriptors;

        public EntityControllerRegistry(IEnumerable<ICrudControllerDescriptor> crudControllerDescriptors)
        {
            
            _crudControllerDescriptors = new ReadOnlyDictionary<string, ICrudControllerDescriptor>(crudControllerDescriptors.ToDictionary(descriptor => descriptor.EntityName));
        }

        public IEnumerable<ICrudControllerDescriptor> CrudControllerDescriptors => _crudControllerDescriptors.Values;

        public ICrudControllerDescriptor this[string name] => _crudControllerDescriptors[name];

        public ICrudTypedControllerDescriptor<TEntity> GetTypedDescriptor<TEntity>()
            where TEntity : BaseEntityGuidKey
        {
            return (ICrudTypedControllerDescriptor<TEntity>) this[typeof(TEntity).Name];
        }
    }
}
