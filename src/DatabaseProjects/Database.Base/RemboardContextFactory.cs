using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Common.Data;
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
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=Remboard2;Integrated Security=True;MultipleActiveResultSets=true");
            IEnumerable<IConfigureModelFeature> resolvers = new IConfigureModelFeature[] {new CommonModule(), new UsersModule()};
            return new RemboardContext(optionsBuilder.Options, resolvers);
        }
    }
}
