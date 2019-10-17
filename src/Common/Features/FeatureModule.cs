using System.Collections.Generic;
using System.Linq;
using Autofac;
using Common.Features.Cruds;
using Common.Features.PermissibleValues;
using Common.Features.ResourcePoints;

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

            foreach (var permissibleValuesControllerConfigurator in RegisterPermissibleValuesControllers())
            {
                permissibleValuesControllerConfigurator.Finish(builder);
            }

            foreach (var pointConfigurator in RegisterResourcePoints())
            {
	            pointConfigurator.Finish(builder);
            }
		}

        protected virtual IEnumerable<ICrudControllerConfigurator> RegisterCrudControllers()
        {
            return Enumerable.Empty<ICrudControllerConfigurator>();
        }

        protected virtual IEnumerable<IResourcePointConfigurator> RegisterResourcePoints()
        {
	        return Enumerable.Empty<IResourcePointConfigurator>();
        }
		
		protected virtual IEnumerable<IPermissibleValuesControllerConfigurator> RegisterPermissibleValuesControllers()
        {
            return Enumerable.Empty<IPermissibleValuesControllerConfigurator>();
        }

        protected void AddMapperProfile<TMapperProfile>(ContainerBuilder builder)
            where TMapperProfile: FeatureMapperProfile
        {
            builder.RegisterType<TMapperProfile>().As<FeatureMapperProfile>();
        }
    }
}
