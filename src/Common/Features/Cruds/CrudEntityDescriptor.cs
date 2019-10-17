using System.Reflection;
using Common.Features.BaseEntity;

namespace Common.Features.Cruds
{
    public class CrudEntityDescriptor<TEntity, TEntityDto, TFilterableEntity> : ICrudEntityDescriptor
        where TEntity:BaseEntityGuidKey
    {
        private readonly string _entityName;

        private readonly string _entityPluralName;

        private readonly TypeInfo _entityTypeInfo;

        private readonly TypeInfo _entityDtoTypeInfo;

        private readonly TypeInfo _filterableEntityTypeInfo;

        public CrudEntityDescriptor(string pluralName)
        {
            _entityName = typeof(TEntity).Name;

            _entityPluralName = pluralName;

			if (string.IsNullOrWhiteSpace(_entityPluralName))
            {
				_entityPluralName = _entityName + "s";
            }

            _entityTypeInfo = typeof(TEntity).GetTypeInfo();
            _entityDtoTypeInfo = typeof(TEntityDto).GetTypeInfo();
            _filterableEntityTypeInfo = typeof(TFilterableEntity).GetTypeInfo();
        }

        public string EntityName => _entityName;

        public string EntityPluralName => _entityPluralName;

        public TypeInfo EntityTypeInfo => _entityTypeInfo;

        public TypeInfo EntityDtoTypeInfo => _entityDtoTypeInfo;

        public TypeInfo FilterableEntityTypeInfo => _filterableEntityTypeInfo;

    }
}
