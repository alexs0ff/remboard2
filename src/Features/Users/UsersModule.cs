using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Data;
using Common.Features;
using Microsoft.EntityFrameworkCore;
using Users.Api;

namespace Users
{
    public sealed class UsersModule: FeatureModule, IConfigureModelFeature
    { 
        protected override void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
        }

        public void OnContextFeatureCreating(ModelBuilder modelBuilder, RemboardContextParameters contextParameters)
        {
			modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
			modelBuilder.ApplyConfiguration(new ProjectRoleConfiguration());
		}
    }
}
