using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Microsoft.EntityFrameworkCore;

namespace Users
{
    public class ModelFeature: IModelFeature
    {
        public void OnContextFeatureCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
