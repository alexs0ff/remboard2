using System.Collections.Generic;
using System.Linq;
using Autofac;
using Common.Features.Cruds;

namespace Common.Features
{
    public abstract class FeatureModule:Module
    {
        protected FeatureModule()
        {
        }
        protected abstract void RegisterServices(ContainerBuilder builder);

        protected override void Load(ContainerBuilder builder)
        {
            RegisterServices(builder);

            if (this is IConfigureModelFeature cmf)
            {
                builder.RegisterInstance(cmf).As<IConfigureModelFeature>();
            }

            foreach (var crudControllerDescriptor in RegisterCrudControllers())
            {
                builder.RegisterInstance(crudControllerDescriptor);
            }
        }

        protected virtual IEnumerable<ICrudControllerDescriptor> RegisterCrudControllers()
        {
            return Enumerable.Empty<ICrudControllerDescriptor>();
        }
    }
}
