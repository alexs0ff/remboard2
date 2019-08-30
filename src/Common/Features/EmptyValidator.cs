using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.BaseEntity;
using FluentValidation;
using FluentValidation.Validators;

namespace Common.Features
{
    public class EmptyValidator<TEntity> : AbstractValidator<TEntity>
      
    {

    }
}
