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
	public class UserDtoValidator : BaseEntityDtoValidator<UserDto>
	{
		public UserDtoValidator()
		{
			RuleFor(u => u.LoginName).NotEmpty();
			RuleFor(u => u.LoginName).Must(login =>
			{
				if (string.IsNullOrWhiteSpace(login))
				{
					return false;
				}

				if (login.Any(i=> !IdentityOptionsParameters.AllowedUserNameCharacters.Contains(i)))
				{
					return false;
				}

				return true;
			}).WithMessage("Incorrect user login");
			RuleFor(u => u.Email).EmailAddress();
			RuleFor(u => u.ProjectRoleId).IsInEnum();
			RuleFor(u => u.FirstName).NotEmpty();
			RuleFor(u => u.LastName).NotEmpty();
		}
	}
}
