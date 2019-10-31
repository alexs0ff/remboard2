using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Users
{
	public class UserBranchConfiguration : IEntityTypeConfiguration<UserBranch>
	{
		/// <summary>
		///     Configures the entity of type <typeparamref name="TEntity" />.
		/// </summary>
		/// <param name="builder"> The builder to be used to configure the entity type. </param>
		public void Configure(EntityTypeBuilder<UserBranch> builder)
		{
			builder.HasKey(ub => new {ub.BranchId, ub.UserId});
			
			builder.HasOne(ub => ub.Branch)
				.WithMany(b => b.UserBranches)
				.HasForeignKey(ub=>ub.BranchId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(ub => ub.User)
				.WithMany(b => b.UserBranches)
				.IsRequired()
				.HasForeignKey(ub => ub.UserId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
