using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Common.Data;
using Common.Extensions;
using Common.Features;

namespace Database.Base
{
    public class BaseContextBuilder: IContextBuilder
    {
        private static Lazy<List<IConfigureModelFeature>> _features = new Lazy<List<IConfigureModelFeature>>(() =>
        {
            //todo: separate to database.common
            var builder = new ContainerBuilder();
            var refAssembyNames = typeof(RemboardContextFactory).Assembly
                .GetReferencedAssemblies();

            foreach (var refAssembyName in refAssembyNames)
            {
                var loadedAssembly = Assembly.Load(refAssembyName);
                builder.RegisterAssemblyTypes(loadedAssembly).Where(t => t.HasImplementation<IConfigureModelFeature>())
                    .As<IConfigureModelFeature>();
            }

            var list = new List<IConfigureModelFeature>(builder.Build().Resolve<IEnumerable<IConfigureModelFeature>>());
            return list;
        });

        public IEnumerable<IConfigureModelFeature> GetFeatures()
        {
            return _features.Value;
        }
    }
}
