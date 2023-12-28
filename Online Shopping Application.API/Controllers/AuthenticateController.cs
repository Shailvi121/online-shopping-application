using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Online_Shopping_Application.API.Models;
using Online_Shopping_Application.API.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Online_Shopping_Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly JWTServices _jwtService;
        private readonly IUserLogin _userLogin;
        private readonly IUserRole _userRoles;

        public AuthenticateController(JWTServices jwtService, IUserLogin userLogin, IUserRole userRoles, IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtService = jwtService;
            _userLogin = userLogin;
            _userRoles = userRoles;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userLogin.FindEmailByAsync(model.Username);
            if (user != null && (await _userLogin.CheckPasswordAsync(user, model.Password)))
            {
                var userRoles = await _userRoles.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Name, model.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                //foreach (var userRole in userRoles)
                //{
                //    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                //}

                return Ok(_jwtService.GenerateToken(authClaims));
            }

            return Unauthorized();
        }
    }
}
