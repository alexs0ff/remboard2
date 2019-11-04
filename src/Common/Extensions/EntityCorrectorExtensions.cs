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
		public static async Task CorrectCreateEntityAsync<TEntity, TCreateEntityDto, TEditEntityDto, TKey>(
			this List<IEntityCorrector<TEntity, TCreateEntityDto,TEditEntityDto, TKey>> correctors, EntityCorrectorContext context, TEntity entity, TCreateEntityDto receivedCreateEntityDto)
			where TEntity : BaseEntity<TKey>
			where TKey : struct
			where TCreateEntityDto : class
			where TEditEntityDto : class
		{
			foreach (var entityCorrector in correctors)
			{
				await entityCorrector.CorrectEntityAsync(context, entity, receivedCreateEntityDto);
			}
		}

		public static async Task CorrectEditEntityAsync<TEntity, TCreateEntityDto, TEditEntityDto, TKey>(
			this List<IEntityCorrector<TEntity, TCreateEntityDto, TEditEntityDto, TKey>> correctors, EntityCorrectorContext context, TEntity entity,TEditEntityDto receivedEditEntityDto)
			where TEntity : BaseEntity<TKey>
			where TKey : struct
			where TCreateEntityDto : class
			where TEditEntityDto : class
		{
			foreach (var entityCorrector in correctors)
			{
				await entityCorrector.CorrectEntityAsync(context, entity, receivedEditEntityDto);
			}
		}


		public static async Task CorrectEditEntityDtoAsync<TEntity, TCreateEntityDto, TEditEntityDto, TKey>(
			this List<IEntityCorrector<TEntity, TCreateEntityDto, TEditEntityDto, TKey>> correctors, EntityCorrectorContext context,TEditEntityDto receivedEditEntityDto, TEntity entity)
			where TEntity : BaseEntity<TKey>
			where TKey : struct
			where TCreateEntityDto : class
			where TEditEntityDto : class
		{
			foreach (var entityCorrector in correctors)
			{
				await entityCorrector.CorrectEntityDtoAsync(context, receivedEditEntityDto, entity);
			}
		}
	}
}
