using System;
using System.Collections.Generic;
using System.Text;
using MassTransit;

namespace Common.MessagingQueue.Consumers
{
	public interface IConsumerBase<in TMessage>: IConsumer<TMessage>
		where TMessage: class,IQueueMessageBase
	{

	}
}
