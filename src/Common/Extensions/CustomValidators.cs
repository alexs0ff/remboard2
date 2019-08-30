using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Common.Extensions
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, Guid> GuidIsNotEmpty<T, TElement>(this IRuleBuilder<T, Guid> ruleBuilder)
        {
            return ruleBuilder.Must(item => item!=Guid.Empty).WithMessage("The Guid is empty");
        }
    }
}
