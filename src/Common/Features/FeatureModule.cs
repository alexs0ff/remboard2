using Autofac;

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
        }

        
    }
}
