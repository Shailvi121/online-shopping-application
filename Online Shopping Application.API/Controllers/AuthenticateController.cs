
using Online_Shopping_Application.API.Services;


namespace Online_Shopping_Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly JWTServices _jwtService;
        private readonly IUserLogin _userLogin;
        

        public AuthenticateController(JWTServices jwtService, IUserLogin userLogin,  IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtService = jwtService;
            _userLogin = userLogin;
          
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userLogin.FindUserByEmailAsync(model.Username);
            if (user != null && user.Password==model.Password)
            {
               
                var authClaims = new List<Claim>
                {
                       new Claim(JwtRegisteredClaimNames.Name, model.Username),
                       new Claim(ClaimTypes.Role, "Admin"),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };


                    return Ok(_jwtService.GenerateToken(authClaims));
            }

            return Unauthorized();
        }
    }
}
