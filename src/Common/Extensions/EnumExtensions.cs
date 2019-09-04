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


        public static List<TEntity> EnumToEntities<TEntity,TEnum>()
            where TEntity : Common.Features.BasePermissibleValue<TEnum>, new()
            where TEnum : Enum
        {
            var list = new List<TEntity>();

            var values = Enum.GetValues(typeof(TEnum));

            foreach (long value in values)
            {
                var name = Enum.GetName(typeof(TEnum), value);
                //todo: https://stackoverflow.com/a/9276348

                var idFromValue = (TEnum)Enum.ToObject(typeof(TEnum), value);
                list.Add(new TEntity() { Code = name, Name = EnumExtensions.GetDescription<TEnum>(value), Id = idFromValue });
            }

            return list;
        }
    }
}
