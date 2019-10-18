using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.PermissibleValues
{
    public interface IPermissibleValuesTypedControllerDescriptor<TEntity,TEnum>: IPermissibleValuesControllerDescriptor
        where TEnum : Enum
        where TEntity : BasePermissibleValue<TEnum>
    {
        IPermissibleValuesProvider<TEntity, TEnum> PermissibleValuesProvider { get; }
    }
}
