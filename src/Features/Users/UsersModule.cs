using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Features;
using Microsoft.EntityFrameworkCore;

namespace Users
{
    public class UsersModule: FeatureModule, IConfigureModelFeature
    { 
        protected override void RegisterServices(ContainerBuilder builder)
        {
            
        }

        public void OnContextFeatureCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectRoleConfiguration());
        }
    }
}
