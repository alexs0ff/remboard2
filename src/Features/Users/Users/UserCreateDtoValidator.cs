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
	public class UserCreateDtoValidator : BaseEntityDtoValidator<UserCreateDto>
	{
		public UserCreateDtoValidator(IUserService userService)
		{
			RuleFor(u => u.Password).Matches(IdentityOptionsParameters.PasswordCheckRegex);
			RuleFor(u => u.LoginName).NotEmpty();
			RuleFor(u => u.LoginName).MaximumLength(50);
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
			RuleFor(u => u.LoginName).MustAsync(async (login, token) =>
			{
				var user = await userService.GetUserByLogin(login);
				return user == null;
			}).WithMessage("User login is exists");

			RuleFor(u => u.Email).EmailAddress();
			RuleFor(u => u.Email).MustAsync(async (email, token) =>
			{
				var user = await userService.GetUserByEmail(email);
				return user == null;
			}).WithMessage("User email is exists, please use another");

			RuleFor(u => u.ProjectRoleId).IsInEnum();
			RuleFor(u => u.FirstName).NotEmpty();
			RuleFor(u => u.LastName).NotEmpty();
		}
	}
}
