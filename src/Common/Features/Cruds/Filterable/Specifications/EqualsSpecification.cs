using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Common.Features.Specifications;

namespace Common.Features.Cruds.Filterable.Specifications
{
    public class EqualsSpecification<T> : ISpecification<T>
        where T : class
    {
        private readonly Type _valueType;

        private readonly object _value;

        private readonly string _propertyName;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public EqualsSpecification(Type valueType, object value, string propertyName)
        {
            _valueType = valueType;
            _value = value;
            _propertyName = propertyName;
        }

        public Expression<Func<T, bool>> IsSatisfiedBy()
        {
            var item = Expression.Parameter(typeof(T), "e");

            var prop = Expression.Property(item, _propertyName);

            var valueConstant = Expression.Constant(_value,_valueType);

            var equal = Expression.Equal(prop, valueConstant);

            var lambda = Expression.Lambda<Func<T, bool>>(equal, item);

            return lambda;
        }
    }
}
