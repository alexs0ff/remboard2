using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Common.Features.PermissibleValues
{
    public interface IPermissibleValuesProvider<TEntity,TEnum>
        where TEnum:Enum
        where TEntity: BasePermissibleValue<TEnum>
    {
        Task<IEnumerable<TEntity>> ReadEntitiesAsync();
    }
}
