using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Composers;

namespace Common.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder AddComposer<TComposer>(this ContainerBuilder containerBuilder)
        where TComposer:IFeaturesComposer,new()
        {
            new TComposer().PopulateServices(containerBuilder);
            return containerBuilder;
        }
    }
}
