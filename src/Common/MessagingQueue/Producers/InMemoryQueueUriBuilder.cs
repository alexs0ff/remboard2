using System;
using System.Collections.Generic;
using System.Text;

namespace Common.MessagingQueue.Producers
{
	public class InMemoryQueueUriBuilder: IQueueUriBuilder
	{
		public Uri Create(string queueName)
		{
			return new Uri($"loopback://localhost/{queueName}");
		}
	}
}
