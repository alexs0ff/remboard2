using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Common.MessagingQueue.Consumers
{
	public interface IMessageConsumerConfigurator
	{
		void Finish(ContainerBuilder builder);
	}
}
