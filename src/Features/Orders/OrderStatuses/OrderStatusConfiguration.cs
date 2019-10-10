using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Orders.OrderStatuses
{
	public class OrderStatusConfiguration : BaseEntityGuidKeyConfiguration<OrderStatus>
	{
		/// <summary>
		///     Configures the entity of type <typeparamref name="TEntity" />.
		/// </summary>
		/// <param name="builder"> The builder to be used to configure the entity type. </param>
		public override void Configure(EntityTypeBuilder<OrderStatus> builder)
		{
			base.Configure(builder);
			builder.Property(p => p.Title).IsRequired();
			builder.HasOne<OrderStatusKind>(i => i.OrderStatusKind).WithMany().HasForeignKey(p => p.OrderStatusKindId).IsRequired();
		}
	}
}
