using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Orders.Branches
{
	public class BranchConfiguration : BaseEntityGuidKeyConfiguration<Branch>
	{
		/// <summary>
		///     Configures the entity of type <typeparamref name="TEntity" />.
		/// </summary>
		/// <param name="builder"> The builder to be used to configure the entity type. </param>
		public override void Configure(EntityTypeBuilder<Branch> builder)
		{
			builder.Property(p => p.Title).IsRequired();
			builder.Property(p => p.LegalName).IsRequired();
			builder.Property(p => p.Address);
			base.Configure(builder);
		}
	}
}
