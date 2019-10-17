using System;
using System.Linq;
using System.Reflection;
using Common.Features.BaseEntity;
using JetBrains.Annotations;

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

        public static Type GetEntityTypeOrNull(this Type child)
        {
	        var currentChild = child;

			while (currentChild != typeof(object))
			{
				if (currentChild.IsGenericType)
				{
					foreach (var currentChildGenericTypeArgument in currentChild.GenericTypeArguments)
					{
						if (currentChildGenericTypeArgument.InheritsOrImplements(typeof(BaseEntity<>)))
						{
							return currentChildGenericTypeArgument;
						}
					}
				}

				currentChild = currentChild.BaseType;

				if (currentChild == null)
					return null;
			}

			return null;
        }

        public static bool InheritsOrImplements(this Type child, Type parent)
        {
	        parent = ResolveGenericTypeDefinition(parent);

	        var currentChild = child.IsGenericType
		        ? child.GetGenericTypeDefinition()
		        : child;

	        while (currentChild != typeof(object))
	        {
		        if (parent == currentChild || HasAnyInterfaces(parent, currentChild))
			        return true;

		        currentChild = currentChild.BaseType != null
		                       && currentChild.BaseType.IsGenericType
			        ? currentChild.BaseType.GetGenericTypeDefinition()
			        : currentChild.BaseType;

		        if (currentChild == null)
			        return false;
	        }
	        return false;
        }

        private static bool HasAnyInterfaces(Type parent, Type child)
        {
	        return child.GetInterfaces()
		        .Any(childInterface =>
		        {
			        var currentInterface = childInterface.IsGenericType
				        ? childInterface.GetGenericTypeDefinition()
				        : childInterface;

			        return currentInterface == parent;
		        });
        }



        private static Type ResolveGenericTypeDefinition(Type parent)
        {
	        var shouldUseGenericType = true;
	        if (parent.IsGenericType && parent.GetGenericTypeDefinition() != parent)
		        shouldUseGenericType = false;

	        if (parent.IsGenericType && shouldUseGenericType)
		        parent = parent.GetGenericTypeDefinition();
	        return parent;
        }
	}
}
