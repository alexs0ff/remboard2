using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Microsoft.EntityFrameworkCore;

namespace Common.Tenant
{
    public class TenantModelFeature: IModelFeature
    {
        public void OnContextFeatureCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TenantEntityConfiguration());
        }
    }
}
