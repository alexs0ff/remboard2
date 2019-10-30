using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;
using Entities;

namespace Common.Features.PermissibleValues
{
    public class ReflectionPermissibleValuesProvider<TEntity, TEnum> : IPermissibleValuesProvider<TEntity,TEnum>
        where TEnum : Enum
        where TEntity : BasePermissibleValue<TEnum>, new()
    {
        private static Lazy<IEnumerable<TEntity>> _entities;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ReflectionPermissibleValuesProvider()
        {
            _entities = new Lazy<IEnumerable<TEntity>>(ValueFactory);
        }

        private IEnumerable<TEntity> ValueFactory()
        {
            return EnumExtensions.EnumToEntities<TEntity, TEnum>();
        }

        public Task<IEnumerable<TEntity>> ReadEntitiesAsync()
        {
            return Task.FromResult(_entities.Value);
        }
    }
}
