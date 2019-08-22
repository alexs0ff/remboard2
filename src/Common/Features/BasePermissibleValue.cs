using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features
{
    public abstract class BasePermissibleValue
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
