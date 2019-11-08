using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Entities;
using Entities.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Users.Api;

namespace Users
{
    public class UserService:IUserService
    {
        private readonly RemboardContext _context;

        private readonly ILogger<UserService> _logger;

        private readonly UserManager<IdentityUser> _userManager;

		public UserService(RemboardContext context, ILogger<UserService> logger, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<User> GetUserByLogin(string login)
        {
            _logger.LogInformation(@"Start get user by login {login}",login);
            var norm = login.ToUpper();
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.IsDeleted == false && u.LoginName.ToUpper() == norm);
        }

		public async Task<User> GetUserByEmail(string email)
		{
			_logger.LogInformation(@"Start get user by email {email}", email);

			if (string.IsNullOrWhiteSpace(email))
			{
				return null;
			}

			var normEmail = email.ToUpper();
			return await _context.Set<User>().FirstOrDefaultAsync(u => u.IsDeleted == false && u.Email.ToUpper() == normEmail);
		}

		public async Task CreateUser(UserCreateDto userCreateDto)
		{
			_logger.LogInformation($"Start create user {userCreateDto.LoginName}");

			try
			{
				var user = new IdentityUser
				{
					UserName = userCreateDto.LoginName,
					Email = userCreateDto.Email,
					PhoneNumber = userCreateDto.Phone
				};
				await _userManager.CreateAsync(user, userCreateDto.Password);
			}
			catch (Exception e)
			{
				_logger.LogError($"Failed create user {userCreateDto.LoginName}",e);
				throw;
			}
			
		}
    }
}
