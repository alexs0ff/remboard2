using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Common.Features.Specifications;

namespace Common.Features.Cruds.Filterable.Specifications
{
    public class ContainsSpecification<T> : ISpecification<T>
        where T:class
    {
        private readonly string _value;

        private readonly string _propertyName;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ContainsSpecification(string value, string propertyName)
        {
            _value = value;
            _propertyName = propertyName;
        }

        public Expression<Func<T, bool>> IsSatisfiedBy()
        {
            var methodInfo = typeof(string).GetMethod("Contains",new Type[] { typeof(string) });

            var item = Expression.Parameter(typeof(T), "e");

            var prop = Expression.Property(item, _propertyName);

            var valueConstant = Expression.Constant(_value);

            var body = Expression.Call(prop, methodInfo, valueConstant);

            var lambda = Expression.Lambda<Func<T, bool>>(body, item);

            return lambda;
        }
    }
}
