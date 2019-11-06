using System.Collections.Generic;
using Autofac;

namespace Common.MessagingQueue.Consumers
{
	public class CrudResourceConsumerConfigurator: IMessageConsumerConfigurator
	{
		private readonly List<ReceiveEndpointDescriptor> _receiveEndpointDescriptors = new List<ReceiveEndpointDescriptor>();


		public ReceiveEndpointConfigurator AddEndpoint(string queueName)
		{
			var endpointDescriptor = new ReceiveEndpointDescriptor {QueueName = queueName};
			var endpointConfigurator = new ReceiveEndpointConfigurator(this, endpointDescriptor);
			_receiveEndpointDescriptors.Add(endpointDescriptor);
			return endpointConfigurator;
		}

		public void Finish(ContainerBuilder builder)
		{
			foreach (var receiveEndpointDescriptor in _receiveEndpointDescriptors)
			{
				builder.RegisterInstance(receiveEndpointDescriptor).As<ReceiveEndpointDescriptor>();
			}
		}
	}
}
