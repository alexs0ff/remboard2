using System;
using System.Reflection;

namespace Common.Extensions
{
    public static class TypeExtensions
    {
        public static bool HasImplementation<TType>(this Type derivedType)
        {
            if (derivedType==null)
            {
                return false;
            }

            return typeof(TType).GetTypeInfo().IsAssignableFrom(derivedType.GetTypeInfo());
        }

        public static Type GetPropertyType(this Type type, string propertyName, bool throwException = false)
        {
            var propertyInfo = type.GetProperty(propertyName);

            if (propertyInfo == null)
            {
                if (throwException)
                {
                    throw new ArgumentException($"property {propertyName} does not exists on type {type.Name}");
                }

                return null;
            }

            return propertyInfo.PropertyType;
        }
    }
}
