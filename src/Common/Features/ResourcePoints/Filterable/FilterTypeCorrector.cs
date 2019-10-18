using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Common.Extensions;

namespace Common.Features.ResourcePoints.Filterable
{
    internal static class FilterTypeCorrector
    {
        public static object ChangeType<TEntity>(string parameterName, string parameterValue)
        {
			var property = typeof(TEntity).GetProperty(parameterName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

			if (property == null)
			{
				return null;
			}

			var targetType = property.PropertyType;

			if (targetType.IsEnum)
			{
				targetType = Enum.GetUnderlyingType(targetType);
			}

			object value = parameterValue;

            if (targetType == typeof(int))
            {
                if (!int.TryParse(parameterValue, NumberStyles.None, CultureInfo.InvariantCulture, out var res))
                {
                    return null;
                }
                value = res;
            }else if (targetType == typeof(long))
            {
                if (!long.TryParse(parameterValue, NumberStyles.None, CultureInfo.InvariantCulture, out var res))
                {
                    return null;
                }
                value = res;
            }
            else if (targetType == typeof(Guid))
            {
				if (!Guid.TryParse(parameterValue,  out var res))
				{
					return null;
				}
				value = res;
			}

            if (value!=null && property.PropertyType.IsEnum)
            {
	            value = Enum.ToObject(property.PropertyType, value);
            }

            return value;
        }
    }
}
