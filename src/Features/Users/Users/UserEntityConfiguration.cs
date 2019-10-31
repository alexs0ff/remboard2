using System;
using System.Collections.Generic;
using System.Text;
using Common.Extensions;
using Common.Features;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Users.Users
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
            
            

            builder.HasOne(p=>p.ProjectRole).WithMany().HasForeignKey(p=>p.ProjectRoleId).IsRequired();
            builder.HasMany(p => p.UserBranches).WithOne(b => b.User);


			builder.HasIndex(p => p.LoginName).AddUniqueWithoutDeleted();
            builder.HasIndex(p => p.Email).AddUniqueWithoutDeleted();
        }
    }
}
