using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;

namespace Common.Data
{
    public interface IContextBuilder
    {
        IEnumerable<IConfigureModelFeature> GetFeatures();
    }
}
