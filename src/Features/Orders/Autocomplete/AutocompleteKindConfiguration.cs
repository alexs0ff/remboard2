using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Orders.Autocomplete
{
    public sealed class AutocompleteKindConfiguration : BasePermissibleValueConfiguration<AutocompleteKind, AutocompleteKinds>
    {
        public override void Configure(EntityTypeBuilder<AutocompleteKind> builder)
        {
            base.Configure(builder);
            FillData(builder);
        }
    }
}
