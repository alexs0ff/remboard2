using System;
using System.Collections.Generic;
using System.Text;
using Common.Features;
using Entities.Dto;
using FluentValidation;

namespace Users.Users
{
	public class UserDtoValidator : BaseEntityDtoValidator<UserDto>
	{
		public UserDtoValidator()
		{
			RuleFor(u => u.LoginName).NotEmpty();
			RuleFor(u => u.Email).EmailAddress();
			RuleFor(u => u.ProjectRoleId).IsInEnum();
			RuleFor(u => u.FirstName).NotEmpty();
			RuleFor(u => u.LastName).NotEmpty();
		}
	}
}
