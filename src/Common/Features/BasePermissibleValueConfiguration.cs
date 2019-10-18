using System;
using System.Collections.Generic;
using System.Text;
using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Features
{
    public abstract class BasePermissibleValueConfiguration<TEntity,TEnum> : IEntityTypeConfiguration<TEntity> where TEntity : BasePermissibleValue<TEnum>,new()
    where TEnum:struct,Enum
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Code).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.HasIndex(p => p.Code).IsUnique();
        }

        protected void FillData(EntityTypeBuilder<TEntity> builder)
        {
            // will be fix on preview 9 https://github.com/aspnet/EntityFrameworkCore/issues/17145


            var list = EnumExtensions.EnumToEntities<TEntity,TEnum>();

            builder.HasData(list);
        }
    }
}
