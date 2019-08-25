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
        TypeInfo TypeInfo { get; }
    }
}
