using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Common;
using Common.Features;
using Users;

namespace Remboard.Infrastructure
{
    public class FeatureRegistry
    {
        private FeatureModule[] GetFeatures()
        {
            //TODO add features here
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
