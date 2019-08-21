using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Common.Features
{
    public interface IModelFeature
    {
        void OnContextFeatureCreating(ModelBuilder modelBuilder);
    }
}
