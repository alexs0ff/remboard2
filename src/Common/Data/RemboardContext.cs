using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Microsoft.EntityFrameworkCore;

namespace Common.Data
{
    public class RemboardContext: DbContext
    {
        private readonly IContextBuilder _contextBuilder;

        public RemboardContext(DbContextOptions<RemboardContext> options,IContextBuilder contextBuilder)
            : base(options)
        {
            _contextBuilder = contextBuilder;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            foreach (var feature in _contextBuilder.GetFeatures())
            {
                feature.OnContextFeatureCreating(modelBuilder);
            }
        }
    }
}
