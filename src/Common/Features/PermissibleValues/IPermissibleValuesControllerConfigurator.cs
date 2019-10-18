using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Common.Features.PermissibleValues
{
    public interface IPermissibleValuesControllerConfigurator
    {
        void Finish(ContainerBuilder builder);
    }
}
