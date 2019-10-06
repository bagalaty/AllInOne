using AllInOne.Contract.V2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AllInOne.Controllers.V2
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion(ApiRoutes.VersionNumber)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login([FromBody]UserDAO login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private string GenerateJSONWebToken(UserDAO userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
             new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
             new Claim(JwtRegisteredClaimNames.Email, userInfo.email),
             new Claim("DateOfJoing", userInfo.DateOfJoing.ToString("yyyy-MM-dd")),
             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        private UserDAO AuthenticateUser(UserDAO login)
        {
            UserDAO user = null;

            //Validate the User Credentials 
            //Demo Purpose, I have Passed HardCoded User Information 
            if (login.Username == "Jignesh")
            {
                user = new UserDAO { Username = "Jignesh Trivedi", email = "test.btest@gmail.com" };
            }
            return user;
        }
    }

}