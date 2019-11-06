using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace Common.MessagingQueue.Producers
{
	public class CommandProducerBase
	{
		private readonly IQueueUriBuilder _queueUriBuilder;
		private readonly ISendEndpointProvider _sendEndpointProvider;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public CommandProducerBase(IQueueUriBuilder queueUriBuilder, ISendEndpointProvider sendEndpointProvider)
		{
			_queueUriBuilder = queueUriBuilder;
			_sendEndpointProvider = sendEndpointProvider;
		}

		public async Task Send<TCommand>(string queueName,TCommand command)
			where TCommand:IMessageQueueBase
		{
			var endPoint = await _sendEndpointProvider.GetSendEndpoint(_queueUriBuilder.Create(queueName));

			await endPoint.Send(command);
		}
	}
}
