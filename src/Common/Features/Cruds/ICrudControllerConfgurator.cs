using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Common.Features.Cruds
{
    public interface ICrudControllerConfgurator
    {
        void Finish(ContainerBuilder builder);
    }
}
