using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Common.Features.Specifications
{
    public interface ISpecification<T>
        where T:class
    {
        Expression<Func<T,bool>> IsSatisfiedBy();
    }
}
