using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Features
{
    public abstract class BasePermissibleValueConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BasePermissibleValue,new()
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Code).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.HasIndex(p => p.Code).IsUnique();
        }

        protected void FillData<TEnum>(EntityTypeBuilder<TEntity> builder)
            where TEnum: struct, Enum
        {
            var values = Enum.GetValues(typeof(TEnum));

            var list = new List<TEntity>();

            foreach (long value in values)
            {
                var name = Enum.GetName(typeof(TEnum), value);
                //todo: https://stackoverflow.com/a/9276348
                list.Add(new TEntity(){Code = name,Name = name,Id = value});
            }

            builder.HasData(list);
        }
    }
}
