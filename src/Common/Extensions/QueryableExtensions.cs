using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Common.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortProperty,
            ListSortDirection sortOrder)
        {
            var type = typeof(T);
            var property = GetPropertyType(type,sortProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = CreateBodyExpression(type,sortProperty,parameter);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var typeArguments = new Type[] { type, property.PropertyType };
            var methodName = sortOrder == ListSortDirection.Ascending ? "OrderBy" : "OrderByDescending";
            var resultExp = Expression.Call(typeof(Queryable), methodName, typeArguments, source.Expression,
                Expression.Quote(orderByExp));

            return source.Provider.CreateQuery<T>(resultExp);
        }

        static Expression CreateBodyExpression(Type type, string propertyName,ParameterExpression param)
        {
            Expression body = param;
            foreach (var member in propertyName.Split('.'))
            {
                body = Expression.PropertyOrField(body, member);
            }
            return body;
        }

        public static PropertyInfo GetPropertyType(Type src, string propName)
        {
            if (src == null) throw new ArgumentException("Type cannot be null.", "src");
            if (propName == null) throw new ArgumentException("Value cannot be null.", "propName");

            if (propName.Contains("."))//complex type nested
            {
                var temp = propName.Split(new char[] { '.' }, 2);

                return GetPropertyType(GetPropertyType(src, temp[0]).PropertyType, temp[1]);
            }
            else
            {
                var prop = src.GetPropertyInfoIgnoreCase(propName);
                return prop;
            }
        }
    }
}
