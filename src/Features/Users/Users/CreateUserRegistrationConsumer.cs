using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.MessagingQueue.Consumers;
using MassTransit;

namespace Users.Users
{
	public class CreateUserRegistrationConsumer: IConsumerBase<CreateUserRegistrationCommand>
	{
		public Task Consume(ConsumeContext<CreateUserRegistrationCommand> context)
		{
			return Task.CompletedTask;
		}
	}
}
