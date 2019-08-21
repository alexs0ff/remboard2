using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Features;

namespace Users
{
    public class UsersModule: FeatureModule
    {
        public UsersModule(RegistrationKind registrationKind) : base(registrationKind)
        {

        }

        protected override void RegisterModel(ContainerBuilder builder)
        {
            
        }

        protected override void RegisterServices(ContainerBuilder builder)
        {
            
        }
    }
}
