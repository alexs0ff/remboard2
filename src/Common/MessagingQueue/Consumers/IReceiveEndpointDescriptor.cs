using System;
using System.Collections.Generic;
using System.Text;
using MassTransit;

namespace Common.MessagingQueue.Consumers
{
	public interface IReceiveEndpointDescriptor
	{
		string QueueName { get; set; }

		IReadOnlyList<IConsumerDescriptor> ConsumerDescriptors { get; set; }

		Action<IReceiveEndpointConfigurator> Config { get; set; }
	}
}
