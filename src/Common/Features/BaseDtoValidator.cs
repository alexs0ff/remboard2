using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Common.Features
{
    public class BaseEntityDtoValidator<TEntityDto>: AbstractValidator<TEntityDto>
    {

    }
}
