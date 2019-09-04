using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Common.Features.BaseEntity;

namespace Common.Features.Cruds
{
    public sealed class EntityControllerRegistry
    {
        private readonly IReadOnlyDictionary<string,ICrudControllerDescriptor> _crudControllerDescriptors;

        public EntityControllerRegistry(IEnumerable<ICrudControllerDescriptor> crudControllerDescriptors)
        {
            _crudControllerDescriptors = new ReadOnlyDictionary<string, ICrudControllerDescriptor>(crudControllerDescriptors.ToDictionary(descriptor => descriptor.EntityName));
        }

        public IEnumerable<ICrudControllerDescriptor> CrudControllerDescriptors => _crudControllerDescriptors.Values;

        public ICrudControllerDescriptor this[string name] => _crudControllerDescriptors[name];

        public bool HasEntity(string name)
        {
            return _crudControllerDescriptors.ContainsKey(name);
        }

        public ICrudTypedControllerDescriptor<TEntity, TEntityDto> GetTypedDescriptor<TEntity, TEntityDto>()
            where TEntity : BaseEntityGuidKey
        {
            return (ICrudTypedControllerDescriptor<TEntity, TEntityDto>) this[typeof(TEntity).Name];
        }

        public IFilterableOperationFeature<TEntity, TFilterableEntity> GetFilterableOperationFeature<TEntity, TFilterableEntity>()
            where TEntity : BaseEntityGuidKey
        {
            return (IFilterableOperationFeature<TEntity, TFilterableEntity>)this[typeof(TEntity).Name];
        }
    }
}
