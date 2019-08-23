using Autofac;
using Common;
using Common.Composers;
using Common.Features;
using Users;

namespace Composer.Base
{
    public class FeaturesComposer: IFeaturesComposer
    {
        private FeatureModule[] GetFeatures()
        {
            //TODO add base features here
            return new FeatureModule[]
            {
                new CommonModule(),
                new UsersModule(),
            };
        }

        public void PopulateServices(ContainerBuilder builder)
        {
            foreach (var featureModule in GetFeatures())
            {
                builder.RegisterModule(featureModule);
            }
        }

    }
}
