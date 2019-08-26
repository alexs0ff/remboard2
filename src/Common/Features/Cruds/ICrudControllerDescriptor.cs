using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.Cruds
{
    public interface ICrudControllerDescriptor
    {
        string EntityName { get; }
        ICrudEntityDescriptor EntityDescriptor { get; }
        AccessRuleMap AccessRules { get; }
    }
}
