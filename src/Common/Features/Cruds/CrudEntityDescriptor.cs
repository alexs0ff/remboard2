using System.Reflection;
using Common.Features.BaseEntity;

namespace Common.Features.Cruds
{
    public class CrudEntityDescriptor<TEntity>: ICrudEntityDescriptor
        where TEntity:BaseEntityGuidKey
    {
        private readonly string _entityName;

        private readonly string _entityPluralName;

        private readonly TypeInfo _typeInfo;

        public CrudEntityDescriptor()
        {
            _entityName = typeof(TEntity).Name;
            _entityPluralName = _entityName + "s";

            _typeInfo = typeof(TEntity).GetTypeInfo();
        }

        public string EntityName => _entityName;

        public string EntityPluralName => _entityPluralName;

        public TypeInfo TypeInfo => _typeInfo;
    }
}
