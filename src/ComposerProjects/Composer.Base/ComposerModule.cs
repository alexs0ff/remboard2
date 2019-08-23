using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common;
using Users;

namespace Composer.Base
{
    public class ComposerModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<CommonModule>();
            builder.RegisterModule<UsersModule>();
        }
    }
}
