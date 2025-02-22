﻿using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Features
{
    public class EntityDtoConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder"> The builder to be used to configure the entity type. </param>
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasNoKey();
        }
    }
}
