using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Features.Cruds
{
    public interface ICrudEntityDescriptor
    {
        string EntityName { get; }
        string EntityPluralName { get; }
        TypeInfo EntityTypeInfo { get; }
        TypeInfo EntityDtoTypeInfo { get; }
        TypeInfo FilterableEntityTypeInfo { get; }
    }
}
