using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Orders.OrderTypes
{
	public class OrderTypeConfiguration : BaseEntityGuidKeyConfiguration<OrderType>
	{
		/// <summary>
		///     Configures the entity of type <typeparamref name="TEntity" />.
		/// </summary>
		/// <param name="builder"> The builder to be used to configure the entity type. </param>
		public override void Configure(EntityTypeBuilder<OrderType> builder)
		{
			builder.Property(p => p.Title).IsRequired();
			base.Configure(builder);
		}
	}
}
