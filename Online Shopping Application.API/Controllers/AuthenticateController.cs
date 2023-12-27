using Online_Shopping_Application.API.Services;
using System.Security.Claims;

namespace Online_Shopping_Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTServices _jwtService;

        public AuthenticateController(JWTServices jwtService,UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _jwtService=jwtService;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Username);
            if (user != null && (await _userManager.CheckPasswordAsync(user, model.Password)))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                      new Claim(ClaimTypes.Name, user.UserName),

                      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }


                //var token = GetToken(authClaims);

                return Ok(_jwtService.GenerateToken(authClaims));
               
            }
            return Unauthorized();

        }
        [HttpPost]
        [Route("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            var userRoles = await _userManager.GetRolesAsync(userExists);


            if (userExists != null && userRoles != null && userRoles.Contains(UserRoles.User))
                return StatusCode(StatusCodes.Status500InternalServerError, new Models.Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
               
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Models.Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok(new Models.Response { Status = "Success", Message = "User created successfully!" });
        }
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                var userRoles = await _userManager.GetRolesAsync(userExists);


                if (userExists != null && userRoles != null && userRoles.Contains(UserRoles.Admin))
                    return StatusCode(StatusCodes.Status500InternalServerError, new Models.Response { Status = "Error", Message = "Admin already exists!" });
            }
            IdentityUser user = new()
            {
               
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Models.Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            return Ok(new Models.Response { Status = "Success", Message = "Admin created successfully!" });
        }
        //private JwtSecurityToken GetToken(List<Claim> authClaims)
        //{
        //    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        //    var token = new JwtSecurityToken(
        //        issuer: _configuration["JWT:Issuer"],
        //        audience: _configuration["JWT:Audience"],
        //        expires: DateTime.Now.AddHours(3),
        //        claims: authClaims,
        //        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        //        );
        //    return token;
        //}
    }
}
