using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Users.Api
{
    public interface IUserService
    {
        Task<User> GetUserByLogin(string login);
        Task<User> GetUserByEmail(string email);
    }
}
