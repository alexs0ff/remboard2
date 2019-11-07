using System;
using System.Collections.Generic;
using System.Text;
using Common.MessagingQueue;

namespace Common.Features.ResourcePoints.Crud.Messaging.Commands
{
	public interface IAfterEntityEditCommand<TEditEntityDto,TKey>: IQueueMessageBase
		where TEditEntityDto : class
		where TKey:struct
	{
		TEditEntityDto EditEntityDto { get; set; }

		TKey Id { get; set; }
	}
}
