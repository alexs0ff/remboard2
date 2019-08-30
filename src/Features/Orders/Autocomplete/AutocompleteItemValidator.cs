using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using FluentValidation;
using FluentValidation.Results;

namespace Orders.Autocomplete
{
    public sealed class AutocompleteItemValidator: BaseEntityGuidKeyValidator<AutocompleteItem>
    {
        public AutocompleteItemValidator()
        {
            RuleFor(i => i.Title).NotEmpty();
            RuleFor(i => i.AutocompleteKindId).IsInEnum();
        }
    }
}
