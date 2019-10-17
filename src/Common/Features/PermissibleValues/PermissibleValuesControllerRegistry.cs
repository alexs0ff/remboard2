using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Common.Features.PermissibleValues
{
    public sealed class PermissibleValuesControllerRegistry
    {
        private readonly IReadOnlyDictionary<string, IPermissibleValuesControllerDescriptor> _permissibleValuesControllerDescriptors;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public PermissibleValuesControllerRegistry(IEnumerable<IPermissibleValuesControllerDescriptor> permissibleValuesControllerDescriptors)
        {
            _permissibleValuesControllerDescriptors = new ReadOnlyDictionary<string, IPermissibleValuesControllerDescriptor>(permissibleValuesControllerDescriptors.ToDictionary(descriptor => descriptor.EntityName));
        }

        public bool HasEntity(string name)
        {
	        return _permissibleValuesControllerDescriptors.ContainsKey(name);
        }

		public IEnumerable<IPermissibleValuesControllerDescriptor> PermissibleValuesControllerDescriptors => _permissibleValuesControllerDescriptors.Values;

        public IPermissibleValuesControllerDescriptor this[string name] => _permissibleValuesControllerDescriptors[name];

        public IPermissibleValuesTypedControllerDescriptor<TEntity, TEnum> GetTypedDescriptor<TEntity, TEnum>()
            where TEnum : Enum
            where TEntity : BasePermissibleValue<TEnum>
        {
            return (IPermissibleValuesTypedControllerDescriptor<TEntity, TEnum>)this[typeof(TEntity).Name];
        }
    }
}
