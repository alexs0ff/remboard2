using System;
using System.Collections.Generic;
using System.Text;
using MassTransit;

namespace Common.MessagingQueue.Consumers
{
	public class ReceiveEndpointDescriptor
	{
		public string QueueName { get; set; }

		public List<ConsumerDescriptor> ConsumerDescriptors { get; set; }

		public Action<IReceiveEndpointConfigurator> Config { get; set; }
	}
}
