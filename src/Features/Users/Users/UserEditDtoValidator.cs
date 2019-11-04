using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Features;
using Common.Features.Auth;
using Entities.Dto;
using FluentValidation;

namespace Users.Users
{
	public class UserEditDtoValidator : BaseEntityDtoValidator<UserEditDto>
	{
		public UserEditDtoValidator()
		{
			RuleFor(u => u.Email).EmailAddress();
			RuleFor(u => u.ProjectRoleId).IsInEnum();
			RuleFor(u => u.FirstName).NotEmpty();
			RuleFor(u => u.LastName).NotEmpty();
		}
	}
}
