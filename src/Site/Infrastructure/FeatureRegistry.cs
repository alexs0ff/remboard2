using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Common.Features;
using Users;

namespace Remboard.Infrastructure
{
    public class FeatureRegistry
    {
        private FeatureModule[] GetFeatures(RegistrationKind registrationKind)
        {
            //TODO add features here
            return new FeatureModule[]
            {
                new UsersModule(registrationKind),
            };
        }

        public void PopulateServices(ContainerBuilder builder)
        {
            foreach (var featureModule in GetFeatures(RegistrationKind.Services))
            {
                builder.RegisterModule(featureModule);
            }
        }

    }
}
