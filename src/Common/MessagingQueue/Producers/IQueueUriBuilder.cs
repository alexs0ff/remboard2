using System;
using System.Collections.Generic;
using System.Text;

namespace Common.MessagingQueue.Producers
{
	public interface IQueueUriBuilder
	{
		Uri Create(string queueName);
	}
}
