using System;
using System.Collections.Generic;
using System.Text;

namespace Common.MessagingQueue.Consumers
{
	public interface IConsumerDescriptor
	{
		Type ConsumerType{ get; set; }
	}
}
