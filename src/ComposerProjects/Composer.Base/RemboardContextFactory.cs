using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Autofac;
using Common;
using Common.Data;
using Common.Extensions;
using Common.Features;
using Composer.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Users;

namespace Database.Base
{
    //from https://docs.microsoft.com/ru-ru/ef/core/miscellaneous/cli/dbcontext-creation#from-a-design-time-factory
    public class RemboardContextFactory: IDesignTimeDbContextFactory<RemboardContext>
    {
        public RemboardContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RemboardContext>();
            //try configure from here https://garywoodfine.com/configuration-api-net-core-console-application/
            optionsBuilder.UseSqlServer(GetConnectionString(), m=>m.MigrationsAssembly(typeof(RemboardContextFactory).Assembly.FullName));
            
            var container = new ContainerBuilder();

            container.RegisterInstance(optionsBuilder.Options);
            container.RegisterModule<ComposerModule>();
            container.RegisterType<RemboardContext>();

            return container.Build().Resolve<RemboardContext>();
        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            return config["ConnectionStrings:RemboardDb"];
        }
    }
}
