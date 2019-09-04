using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Common.Features.Cruds
{
    public interface ICrudControllerConfigurator
    {
        void Finish(ContainerBuilder builder);
    }
}
