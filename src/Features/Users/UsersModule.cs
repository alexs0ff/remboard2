﻿using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Features;

namespace Users
{
    public class UsersModule: FeatureModule
    { 
        protected override void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<ModelFeature>().As<IModelFeature>();
        }
    }
}
