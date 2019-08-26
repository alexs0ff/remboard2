﻿using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Features;
using Common.Features.Cruds;
using Common.Tenant;
using Microsoft.EntityFrameworkCore;

namespace Common
{
    public class CommonModule: FeatureModule, IConfigureModelFeature
    {
        protected override void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<EntityControllerRegistry>();
        }

        public void OnContextFeatureCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TenantEntityConfiguration());
        }
    }
}
