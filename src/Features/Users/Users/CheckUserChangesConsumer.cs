using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.MessagingQueue.Consumers;
using MassTransit;

namespace Users.Users
{
	public class CheckUserChangesConsumer : IConsumerBase<CheckUserChangesCommand>
	{
		public Task Consume(ConsumeContext<CheckUserChangesCommand> context)
		{
			return Task.CompletedTask;
		}
	}
}
