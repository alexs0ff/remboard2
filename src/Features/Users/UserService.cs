using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Users.Api;

namespace Users
{
    public class UserService:IUserService
    {
        private readonly RemboardContext _context;

        private readonly ILogger<UserService> _logger;

        public UserService(RemboardContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
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

    }
}
