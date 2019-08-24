using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Features;
using Microsoft.EntityFrameworkCore;
using Orders.Autocomplete;

namespace Orders
{
    public sealed class OrdersModule : FeatureModule, IConfigureModelFeature
    {
        protected override void RegisterServices(ContainerBuilder builder)
        {
            
        }

        public void OnContextFeatureCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AutocompleteItemConfiguration());
            modelBuilder.ApplyConfiguration(new AutocompleteKindConfiguration());
        }
    }
}
