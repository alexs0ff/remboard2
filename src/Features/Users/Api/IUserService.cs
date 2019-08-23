using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Users.Api
{
    public interface IUserService
    {
        Task<User> GetUserByLogin(string login);
    }
}
