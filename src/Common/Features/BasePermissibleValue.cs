using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features
{
    public abstract class BasePermissibleValue<TEnum>
    where TEnum:Enum
    {
        public TEnum Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
