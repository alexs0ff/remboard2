using System;
using System.Collections.Generic;
using System.Text;
using Common.Data;
using Common.Features;
using Microsoft.EntityFrameworkCore;

namespace Common.Extensions
{
	public static class ModelBuilderExtensions
	{
		/// <summary>
		/// Only when TEntityDto use as typed parameter FromSqlRaw method of entity context
		/// </summary>
		public static void ApplyEntityDtoConfiguration<TEntityDto>(this ModelBuilder modelBuilder,
			RemboardContextParameters contextParameters)
			where TEntityDto:class
		{
			if (!contextParameters.IsDesignTime)
			{
				modelBuilder.ApplyConfiguration(new EntityDtoConfiguration<TEntityDto>());
			}
		}
	}
}
