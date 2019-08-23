using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Users.Api;

namespace Remboard.Auth
{
    //[Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly IUserService _userService;


        public LoginController(IConfiguration config, UserManager<IdentityUser> userManager, IUserService userService)
        {
            _config = config;
            _userManager = userManager;
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Post([FromBody]UserModel login)
        {
            IActionResult response = Unauthorized();
            var user = await AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = await GenerateJSONWebToken(user);
                response = Ok(new { access_token = tokenString });
            }


            return response;
        }

        private async Task<string> GenerateJSONWebToken(IdentityUser userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsIdentity = await GetIdentity(userInfo);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claimsIdentity.Claims,
                expires: DateTime.Now.AddDays(14),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<ClaimsIdentity> GetIdentity(IdentityUser userInfo)
        {
            if (userInfo != null)
            {
                var user = await _userService.GetUserByLogin(userInfo.UserName);

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userInfo.UserName),
                    //new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin1"),
                    //new Claim(ClaimsIdentity.DefaultRoleClaimType, "testrole2")
                };

                if (user!=null)
                {
                    claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType,user.ProjectRoleId.ToString()));
                }
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        private async Task<IdentityUser> AuthenticateUser(UserModel login)
        {
            var fu = await _userManager.FindByNameAsync(login.Login);

            if (fu != null && await _userManager.CheckPasswordAsync(fu, password: login.Password))
            {
                return fu;
            }

            return null;
        }
    }
}
