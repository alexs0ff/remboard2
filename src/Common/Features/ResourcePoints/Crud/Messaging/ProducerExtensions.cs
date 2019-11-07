using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Features.ResourcePoints.Crud.Messaging
{
	public static class ProducerExtensions
	{
		public static async Task SendToAll<TCreateEntityDto, TKey>(
			this List<IAfterEntityCreateCommandProducer<TCreateEntityDto,TKey>> producers, TCreateEntityDto createdEntity,TKey id)
			where TCreateEntityDto : class
			where TKey : struct
		{
			if (producers == null)
			{
				return;
			}

			foreach (var afterEntityCreateCommandProducer in producers)
			{
				await afterEntityCreateCommandProducer.Send(createdEntity,id);
			}
		}

		public static async Task SendToAll<TEditEntityDto,TKey>(
			this List<IAfterEntityEditCommandProducer<TEditEntityDto,TKey>> producers, TEditEntityDto entity, TKey id)
			where TEditEntityDto : class
			where TKey : struct
		{
			if (producers == null)
			{
				return;
			}

			foreach (var afterEntityEditCommandProducer in producers)
			{
				await afterEntityEditCommandProducer.Send(entity,id);
			}
		}
	}
}
