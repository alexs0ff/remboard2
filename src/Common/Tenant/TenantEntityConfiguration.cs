using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Tenant
{
    public class TenantEntityConfiguration: BaseEntityGuidKeyConfiguration<Tenant>
    {
        public override void Configure(EntityTypeBuilder<Tenant> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.RegistredEmail).IsRequired();
            builder.Property(p => p.IsActive);
            builder.Property(p => p.LegalName);
            builder.Property(p => p.Trademark);
            builder.Property(p => p.Address);
            builder.Property(p => p.UserLogin);
            builder.Property(p => p.Number).UseIdentityColumn();
        }
    }
}
