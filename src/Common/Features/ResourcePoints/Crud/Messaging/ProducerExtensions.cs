using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Features.ResourcePoints.Crud.Messaging
{
	public static class ProducerExtensions
	{
		public static async Task SendToAll<TCreateEntityDto>(
			this List<IAfterCreateEntityCommandProducer<TCreateEntityDto>> producers, TCreateEntityDto createdEntity)
			where TCreateEntityDto : class
		{
			if (producers == null)
			{
				return;
			}

			foreach (var afterCreateEntityCommandProducer in producers)
			{
				await afterCreateEntityCommandProducer.Send(createdEntity);
			}
		}
	}
}
