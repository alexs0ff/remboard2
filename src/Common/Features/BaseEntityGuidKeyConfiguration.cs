using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Features
{
    public abstract class BaseEntityGuidKeyConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntityGuidKey
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.RowVersion).IsRowVersion();
            builder.Property(p => p.IsDeleted);
            builder.Property(p => p.DateCreated).HasDefaultValueSql("GETDATE()");

        }
    }
}
