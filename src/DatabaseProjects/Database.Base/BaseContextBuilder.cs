using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Common;
using Common.Data;
using Common.Extensions;
using Common.Features;
using Users;

namespace Database.Base
{
    //TODO move databaseprojects to featurecomposer projects
    public class BaseContextBuilder: IContextBuilder
    {
        private static Lazy<List<IConfigureModelFeature>> _features = new Lazy<List<IConfigureModelFeature>>(() =>
        {
            return new List<IConfigureModelFeature>
            {
                new UsersModule(),
                new CommonModule()
            };
        });

        public IEnumerable<IConfigureModelFeature> GetFeatures()
        {
            return _features.Value;
        }
    }
}
