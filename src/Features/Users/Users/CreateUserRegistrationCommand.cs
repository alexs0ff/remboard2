using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.ResourcePoints.Crud.Messaging;
using Entities.Dto;

namespace Users.Users
{
	public class CreateUserRegistrationCommand: IAfterEntityCreatedCommand<UserCreateDto>
	{
		public Guid CorrelationId { get; set; }

		public UserCreateDto CreatedEntityDto { get; set; }
	}
}
