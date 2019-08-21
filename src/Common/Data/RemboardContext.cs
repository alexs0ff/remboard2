using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Microsoft.EntityFrameworkCore;

namespace Common.Data
{
    public class RemboardContext: DbContext
    {
        private readonly IEnumerable<IModelFeature> _features;

        public RemboardContext(DbContextOptions<RemboardContext> options,IEnumerable<IModelFeature> features)
            : base(options)
        {
            _features = features;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            foreach (var feature in _features)
            {
                feature.OnContextFeatureCreating(modelBuilder);
            }
        }
    }
}
