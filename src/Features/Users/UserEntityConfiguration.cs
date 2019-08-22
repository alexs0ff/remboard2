using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Users
{
    public class UserEntityConfiguration: BaseEntityGuidKeyConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.LoginName).IsRequired();
            builder.Property(p => p.FirstName).IsRequired();
            builder.Property(p => p.LastName).IsRequired();
            builder.Property(p => p.MiddleName);
            builder.Property(p => p.Phone);
        }
    }
}
