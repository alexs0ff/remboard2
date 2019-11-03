using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Features;
using Entities;

namespace Common.Extensions
{
	public static class EntityCorrectorExtensions
	{
		public static async Task CorrectEntityAsync<TEntity, TEntityDto, TKey>(
			this List<IEntityCorrector<TEntity, TEntityDto, TKey>> correctors, EntityCorrectorContext context, TEntity entity, TEntityDto receivedEntityDto)
			where TEntity : BaseEntity<TKey>
			where TKey : struct
		{
			foreach (var entityCorrector in correctors)
			{
				await entityCorrector.CorrectEntityAsync(context, entity, receivedEntityDto);
			}
		}

		public static async Task CorrectEntityDtoAsync<TEntity, TEntityDto, TKey>(
			this List<IEntityCorrector<TEntity, TEntityDto, TKey>> correctors, EntityCorrectorContext context, TEntityDto entityDto, TEntity entity)
			where TEntity : BaseEntity<TKey>
			where TKey : struct
		{
			foreach (var entityCorrector in correctors)
			{
				await entityCorrector.CorrectEntityDtoAsync(context,entityDto, entity);
			}
		}
	}
}
