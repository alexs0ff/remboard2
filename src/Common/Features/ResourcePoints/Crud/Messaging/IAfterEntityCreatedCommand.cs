using System;
using System.Collections.Generic;
using System.Text;
using Common.MessagingQueue;

namespace Common.Features.ResourcePoints.Crud.Messaging
{
	public interface IAfterEntityCreatedCommand<TCreateEntityDto> :IMessageQueueBase
		where TCreateEntityDto : class
	{
		TCreateEntityDto CreatedEntityDto { get; set; }
	}
}
