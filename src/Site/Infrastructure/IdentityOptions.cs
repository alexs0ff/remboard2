using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Features.Auth;
using Microsoft.AspNetCore.Identity;

namespace Remboard.Infrastructure
{
	public static class IdentityOptionsConfigurator
	{
		public static void Configure(IdentityOptions options)
		{
			//https://blog.dangl.me/archive/validating-usernames-in-an-angular-frontend-with-aspnet-core-identity-on-the-server/
			options.User.AllowedUserNameCharacters = IdentityOptionsParameters.AllowedUserNameCharacters;
			options.Password.RequireDigit = true;
			options.Password.RequireLowercase = true;
			options.Password.RequireUppercase = true;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequiredLength = 8;
			options.Password.RequiredUniqueChars = 1;
			//options.Password.
		}
	}
}
