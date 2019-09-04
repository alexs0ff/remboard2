using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.Cruds;

namespace Common.Features.PermissibleValues
{
    public interface IPermissibleValuesControllerDescriptor
    {
        IPermissibleValuesDescriptor PermissibleValuesDescriptor { get; }
        AccessRuleMap AccessRules { get; }

        string EntityName { get; }
    }
}
