using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.ResourcePoints.Crud.Messaging.Commands;
using Entities.Dto;

namespace Users.Users
{
	public class CheckUserChangesCommand: IAfterEntityEditCommand<UserEditDto, Guid>
	{
		public Guid CorrelationId { get; set; }

		public UserEditDto EditEntityDto { get; set; }

		public Guid Id { get; set; }
	}
}
