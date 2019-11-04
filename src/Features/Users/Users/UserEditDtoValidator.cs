using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Features;
using Common.Features.Auth;
using Entities.Dto;
using FluentValidation;
using Users.Api;

namespace Users.Users
{
	public class UserEditDtoValidator : BaseEntityDtoValidator<UserEditDto>
	{
		public UserEditDtoValidator(IUserService userService)
		{
			RuleFor(u => u.Email).EmailAddress();
			RuleFor(u => u.Email).MustAsync(async (dto,email, token) =>
			{
				var user = await userService.GetUserByEmail(email);
				if (user == null)
				{
					return true;
				}

				if (dto.Id == user.Id)
				{
					return true;
				}

				return false;

			}).WithMessage("User email is exists, please use another");
			RuleFor(u => u.ProjectRoleId).IsInEnum();
			RuleFor(u => u.FirstName).NotEmpty();
			RuleFor(u => u.LastName).NotEmpty();
		}
	}
}
