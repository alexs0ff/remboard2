using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<TEnum>(object value)
            where TEnum:Enum
        {
            var type = typeof(TEnum);
            var name = Enum.GetName(typeof(TEnum), value);

            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            var memInfo = type.GetMember(name);
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? ((DescriptionAttribute)attributes[0])?.Description : null;
        }
    }
}
