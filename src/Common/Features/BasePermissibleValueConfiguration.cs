using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Features
{
    public abstract class BasePermissibleValueConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BasePermissibleValue
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Code).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.HasIndex(p => p.Code).IsUnique();
        }
    }
}
