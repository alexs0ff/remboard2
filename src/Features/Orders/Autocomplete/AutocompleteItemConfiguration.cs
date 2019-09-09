using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Orders.Autocomplete
{
    public sealed class AutocompleteItemConfiguration: BaseEntityGuidKeyConfiguration<AutocompleteItem>
    {
        public override void Configure(EntityTypeBuilder<AutocompleteItem> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Title).IsRequired();
            builder.HasOne<AutocompleteKind>(i=>i.AutocompleteKind).WithMany().HasForeignKey(p => p.AutocompleteKindId).IsRequired();
        }
    }
}
