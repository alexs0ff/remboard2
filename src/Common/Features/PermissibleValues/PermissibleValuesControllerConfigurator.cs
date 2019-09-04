using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Common.FeatureEntities;
using Common.Features.Cruds;

namespace Common.Features.PermissibleValues
{
    public class PermissibleValuesControllerConfigurator<TEntity, TEnum>: IPermissibleValuesControllerConfigurator
        where TEnum:Enum
        where TEntity : BasePermissibleValue<TEnum>
    {
        private readonly HashSet<ProjectRoles> _readRoles = new HashSet<ProjectRoles>();

        private Type _permissibleValuesProvider;

        public PermissibleValuesControllerConfigurator<TEntity, TEnum> AddValuesProvider<TProvider>()
            where TProvider: IPermissibleValuesProvider<TEntity, TEnum>
        {
            _permissibleValuesProvider = typeof(TProvider);
            return this;
        }
        public PermissibleValuesControllerConfigurator<TEntity, TEnum> AddReadRoles(params ProjectRoles[] roles)
        {
            AppendRoles(_readRoles, roles);
            return this;
        }

        private void AppendRoles(HashSet<ProjectRoles> roles, ProjectRoles[] rolesToAppend)
        {
            foreach (var projectRole in rolesToAppend)
            {
                if (!roles.Contains(projectRole))
                {
                    roles.Add(projectRole);
                }
            }
        }

        public void Finish(ContainerBuilder builder)
        {
            if (_permissibleValuesProvider == null)
            {
                throw new InvalidOperationException("Need to call AddValuesProvider");
            }

            builder.RegisterType<PermissibleValuesControllerDescriptor<TEntity, TEnum>>()
                .As<IPermissibleValuesControllerDescriptor>()
                .WithParameter("permissibleValuesDescriptor", new PermissibleValuesDescriptor<TEntity, TEnum>())
                .WithParameter("accessRuleMap", new AccessRuleMap(_readRoles.ToArray()))
                .WithParameter("permissibleValuesProviderType", _permissibleValuesProvider)
                .SingleInstance();
        }
    }
}
