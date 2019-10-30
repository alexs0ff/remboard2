using System;
using System.Linq;
using System.Reflection;
using Common.Features.BaseEntity;
using Entities;
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

        public static bool PropertyExists(this Type type, string name)
        {
	        var info = GetPropertyInfoIgnoreCase(type, name);
	        return info != null;
        }

        public static PropertyInfo GetPropertyInfoIgnoreCase(this Type type, string name)
        {
	        return type.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
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
