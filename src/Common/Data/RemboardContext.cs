using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Features;
using Microsoft.EntityFrameworkCore;

namespace Common.Data
{
    public class RemboardContext: DbContext
    {
        private readonly IEnumerable<IConfigureModelFeature> _features;

        private readonly RemboardContextParameters _contextParameters;

        public RemboardContext(DbContextOptions<RemboardContext> options, IEnumerable<IConfigureModelFeature> features, RemboardContextParameters contextParameters)
            : base(options)
        {
	        _features = features;
	        _contextParameters = contextParameters;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
	        var features = _features;

			foreach (var feature in features)
            {
                feature.OnContextFeatureCreating(modelBuilder,_contextParameters);
            }
        }
    }
}
