using System;
using System.Collections.Generic;
using System.Text;
using MassTransit;

namespace Common.MessagingQueue.Consumers
{
	public class ReceiveEndpointConfigurator
	{
		private readonly CrudResourceConsumerConfigurator _configurator;

		private readonly ReceiveEndpointDescriptor _descriptor;


		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public ReceiveEndpointConfigurator(CrudResourceConsumerConfigurator configurator, ReceiveEndpointDescriptor descriptor)
		{
			_configurator = configurator;
			_descriptor = descriptor;
		}

		public ReceiveEndpointConfigurator Configure(Action<IReceiveEndpointConfigurator> cfg)
		{
			_descriptor.Config = cfg;

			return this;
		}

		public ReceiveEndpointConfigurator AddConsumer<TConsumerBase,TMessage>()
			where TConsumerBase: IConsumerBase<TMessage>
			where TMessage : class, IQueueMessageBase
		{
			_descriptor.ConsumerDescriptors.Add(new ConsumerDescriptor
			{
				ConsumerType = typeof(TConsumerBase)
			});
			return this;
		}

		public CrudResourceConsumerConfigurator CompleteEndpoint()
		{
			return _configurator;
		}

	}
}
