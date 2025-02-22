﻿using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Entities.Dto;
using FluentValidation;
using FluentValidation.Results;

namespace Orders.Autocomplete
{
    public sealed class AutocompleteItemDtoValidator: BaseEntityDtoValidator<AutocompleteItemDto>
    {
        public AutocompleteItemDtoValidator()
        {
            RuleFor(i => i.Title).NotEmpty();
            RuleFor(i => i.AutocompleteKindId).IsInEnum();
        }
    }
}
