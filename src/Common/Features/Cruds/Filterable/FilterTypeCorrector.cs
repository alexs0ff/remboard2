using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Common.Extensions;

namespace Common.Features.Cruds.Filterable
{
    internal static class FilterTypeCorrector
    {
        public static object ChangeType<TEntity>(string parameterName, string parameterValue)
        {
            var targetType = typeof(TEntity).GetPropertyType(parameterName);

            if (targetType == null)
            {
                return null;
            }

            object value = parameterValue;

            if (targetType == typeof(int))
            {
                if (!int.TryParse(parameterValue, NumberStyles.None, CultureInfo.InvariantCulture, out var res))
                {
                    return null;
                }
                value = res;
            }

            if (targetType == typeof(long))
            {
                if (!long.TryParse(parameterValue, NumberStyles.None, CultureInfo.InvariantCulture, out var res))
                {
                    return null;
                }
                value = res;
            }

            return value;
        }
    }
}
