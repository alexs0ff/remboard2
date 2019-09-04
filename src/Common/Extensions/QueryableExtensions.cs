﻿using System;
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
            var property = type.GetProperty(sortProperty, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var typeArguments = new Type[] {type, property.PropertyType};
            var methodName = sortOrder == ListSortDirection.Ascending ? "OrderBy" : "OrderByDescending";
            var resultExp = Expression.Call(typeof(Queryable), methodName, typeArguments, source.Expression,
                Expression.Quote(orderByExp));

            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}
