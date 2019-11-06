using System;
using System.Collections.Generic;
using System.Text;

namespace Common.MessagingQueue
{
	public interface IQueueMessageBase
	{
		Guid CorrelationId { get; set; }
	}
}
