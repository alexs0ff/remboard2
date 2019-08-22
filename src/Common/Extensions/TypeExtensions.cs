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
    }
}
