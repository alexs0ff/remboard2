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

            foreach (var controllerConfgurator in RegisterCrudControllers())
            {
                controllerConfgurator.Finish(builder);
            }
        }

        protected virtual IEnumerable<ICrudControllerConfgurator> RegisterCrudControllers()
        {
            return Enumerable.Empty<ICrudControllerConfgurator>();
        }
    }
}
