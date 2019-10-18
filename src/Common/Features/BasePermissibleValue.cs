using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.BaseEntity;

namespace Common.Features
{
    public abstract class BasePermissibleValue<TEnum>:BaseEntity<TEnum>
    where TEnum: struct, Enum
    {

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
