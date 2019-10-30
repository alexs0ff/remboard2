using System;
using System.Collections.Generic;
using Entities;
using Autofac;

namespace Common.Features.PermissibleValues
{
    public class PermissibleValuesControllerDescriptor<TEntity, TEnum> : IPermissibleValuesTypedControllerDescriptor<TEntity,TEnum>
        where TEnum:Enum
        where TEntity : BasePermissibleValue<TEnum>
    {
        private readonly Type _permissibleValuesProviderType;

        private Lazy<IPermissibleValuesProvider<TEntity, TEnum>> _permissibleValuesProvider;

        private readonly IComponentContext _context;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public PermissibleValuesControllerDescriptor(AccessRuleMap accessRuleMap, IPermissibleValuesDescriptor permissibleValuesDescriptor, Type permissibleValuesProviderType, IComponentContext context)
        {
            _permissibleValuesProviderType = permissibleValuesProviderType;
            _context = context;
            AccessRules = accessRuleMap;
            PermissibleValuesDescriptor = permissibleValuesDescriptor;
            _permissibleValuesProvider = new Lazy<IPermissibleValuesProvider<TEntity, TEnum>>(PermissibleValuesProviderFactory);
        }

        private IPermissibleValuesProvider<TEntity, TEnum> PermissibleValuesProviderFactory()
        {
            return (IPermissibleValuesProvider<TEntity, TEnum>)_context.Resolve(_permissibleValuesProviderType);
        }

        public IPermissibleValuesDescriptor PermissibleValuesDescriptor { get; }

        public AccessRuleMap AccessRules { get; }

        public string EntityName => PermissibleValuesDescriptor.EntityName;

        public IPermissibleValuesProvider<TEntity, TEnum> PermissibleValuesProvider => _permissibleValuesProvider.Value;
    }
}
