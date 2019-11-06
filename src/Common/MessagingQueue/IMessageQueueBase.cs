using System;
using System.Collections.Generic;
using System.Text;

namespace Common.MessagingQueue
{
	public interface IMessageQueueBase
	{
		Guid CorrelationId { get; set; }
	}
}
