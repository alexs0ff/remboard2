using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.MessagingQueue.Consumers;
using MassTransit;
using Users.Api;

namespace Users.Users
{
	public class CreateUserRegistrationConsumer: IConsumerBase<CreateUserRegistrationCommand>
	{
		private readonly IUserService _userService;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public CreateUserRegistrationConsumer(IUserService userService)
		{
			_userService = userService;
		}

		public async Task Consume(ConsumeContext<CreateUserRegistrationCommand> context)
		{
			await _userService.CreateUser(context.Message.CreatedEntityDto);
		}
	}
}
