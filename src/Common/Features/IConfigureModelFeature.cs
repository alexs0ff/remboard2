using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Common.Features
{
    public interface IConfigureModelFeature
    {
        void OnContextFeatureCreating(ModelBuilder modelBuilder);
    }
}
