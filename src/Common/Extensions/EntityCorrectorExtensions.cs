using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Entities;

namespace Common.Extensions
{
	public static class EntityCorrectorExtensions
	{
		public static void CorrectEntityAsync<TEntity, TEntityDto, TKey>(
			this List<IEntityCorrector<TEntity, TEntityDto, TKey>> correctors, TEntity entity, TEntityDto receivedEntityDto)
			where TEntity : BaseEntity<TKey>
			where TKey : struct
		{
			foreach (var entityCorrector in correctors)
			{
				entityCorrector.CorrectEntityAsync(entity, receivedEntityDto);
			}
		}

		public static void CorrectEntityDtoAsync<TEntity, TEntityDto, TKey>(
			this List<IEntityCorrector<TEntity, TEntityDto, TKey>> correctors, TEntityDto entityDto, TEntity entity)
			where TEntity : BaseEntity<TKey>
			where TKey : struct
		{
			foreach (var entityCorrector in correctors)
			{
				entityCorrector.CorrectEntityDtoAsync(entityDto, entity);
			}
		}
	}
}
