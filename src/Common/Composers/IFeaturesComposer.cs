using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Common.Composers
{
    public interface IFeaturesComposer
    {
        void PopulateServices(ContainerBuilder builder);
    }
}
