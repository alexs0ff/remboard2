using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.PermissibleValues
{
    public interface IPermissibleValuesDescriptor
    {
        string EntityName { get; }
        string EntityPluralName { get; }
    }
}
