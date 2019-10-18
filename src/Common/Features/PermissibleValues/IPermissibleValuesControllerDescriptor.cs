using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.PermissibleValues
{
    public interface IPermissibleValuesControllerDescriptor
    {
        IPermissibleValuesDescriptor PermissibleValuesDescriptor { get; }
        AccessRuleMap AccessRules { get; }

        string EntityName { get; }
    }
}
