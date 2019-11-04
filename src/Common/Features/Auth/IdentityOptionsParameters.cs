using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.Auth
{
	public class IdentityOptionsParameters
	{
		public const string AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

		/// <summary>
		/// <remarks>
		///^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$
		///^: first line
		///(?=.*[a-z]) : Should have at least one lower case
		///(?=.*[A-Z]) : Should have at least one upper case
		///(?=.*\d) : Should have at least one number
		///(?=.*[#$^+=!*()@%&] ) : Should have at least one special character
		///.{ 8,} : Minimum 8 characters
		///$ : end line
		/// </remarks>
		/// </summary>
		public const string PasswordCheckRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
	}
}
