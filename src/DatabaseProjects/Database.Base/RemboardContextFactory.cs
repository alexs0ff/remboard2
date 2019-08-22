using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Common;
using Common.Data;
using Common.Extensions;
using Common.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=Remboard2;Integrated Security=True;MultipleActiveResultSets=true",m=>m.MigrationsAssembly(typeof(RemboardContextFactory).Assembly.FullName));

            var builder = new ContainerBuilder();
            var refAssembyNames = typeof(RemboardContextFactory).Assembly
                .GetReferencedAssemblies();

            foreach (var refAssembyName in refAssembyNames)
            {
                var loadedAssembly = Assembly.Load(refAssembyName);
                builder.RegisterAssemblyTypes(loadedAssembly).Where(t => t.HasImplementation<IConfigureModelFeature>())
                    .As<IConfigureModelFeature>();
            }

            builder.RegisterInstance(optionsBuilder.Options);
            return builder.Build().Resolve<RemboardContext>();
        }
    }
}
