using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace Common.MessagingQueue.Producers
{
	public class EventProducerBase
	{
		private readonly IPublishEndpoint _publishEndpoint;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public EventProducerBase(IPublishEndpoint publishEndpoint)
		{
			_publishEndpoint = publishEndpoint;
		}

		public async Task Publish<TEvent>(TEvent eventMessage)
			where TEvent: class, IMessageQueueBase
		{
			await _publishEndpoint.Publish<TEvent>(eventMessage);
		}
	}
}
