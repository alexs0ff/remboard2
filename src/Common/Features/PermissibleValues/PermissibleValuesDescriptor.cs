using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Features.PermissibleValues
{
    public class PermissibleValuesDescriptor<TEntity, TEnum> : IPermissibleValuesDescriptor
        where TEnum:struct,Enum
        where TEntity : BasePermissibleValue<TEnum>
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public PermissibleValuesDescriptor()
        {
            EntityName = typeof(TEntity).Name;
            EntityPluralName = typeof(TEnum).Name;
            EntityTypeInfo = typeof(TEntity).GetTypeInfo();
            EntityEnumInfo = typeof(TEnum).GetTypeInfo();
        }

        public string EntityName { get; }

        public string EntityPluralName { get; }

        public TypeInfo EntityTypeInfo { get; }

        public TypeInfo EntityEnumInfo { get; }
    }
}
