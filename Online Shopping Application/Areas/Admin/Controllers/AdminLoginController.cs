using Online_Shopping_Application.Area.Admin.Model;

namespace Online_Shopping_Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    
   
    public class AdminLoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpAPIWrapper _apiWrapper;


        public AdminLoginController(IConfiguration configuration, HttpAPIWrapper apiWrapper)
        {
            _configuration = configuration;
            _apiWrapper = apiWrapper;

        }
        //  [Route("admin-sign-in")]
       
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            var endpoint = Constants.APIEndpoints.Login;
            var content = new AdminLoginModel
            {
                Username = username,
                Password = password,
            };
            var response = await _apiWrapper.PostAsync<TokenResponse, AdminLoginModel>(endpoint, content);
            if (response != null)
            {
                var tokenData = response.data;

                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenparameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration.GetValue<string>("JWT:Issuer"),
                    ValidAudience = _configuration.GetValue<string>("JWT:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")))
                };
                SecurityToken ValidatedToken;
                var principal = tokenHandler.ValidateToken(tokenData.Token, tokenparameters, out ValidatedToken);

                var adminId = principal.FindFirst(ClaimTypes.Name)?.Value;
                var adminRole = principal.FindFirst(ClaimTypes.Role)?.Value;
                Console.WriteLine($"Admin Role: {adminRole}");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
            return RedirectToAction("Index", "Dashboard");
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterAPIModel model)
        {
            var endpoint = Constants.APIEndpoints.Register;
            var content = new RegisterAPIModel
            {
                Username = model.Username,
                Password = model.Password,
            };
            var response = await _apiWrapper.PostAsync<TokenResponse, RegisterAPIModel>(endpoint, content);
            if (response != null)
            {
                var tokenData = response.data;


                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenparameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration.GetValue<string>("JWT:Issuer"),
                    ValidAudience = _configuration.GetValue<string>("JWT:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")))

                };

                SecurityToken ValidatedToken;
                var principal = tokenHandler.ValidateToken(tokenData.Token, tokenparameters, out ValidatedToken);

                var userId = principal.FindFirst(ClaimTypes.Name)?.Value;
                var userRole = principal.FindFirst(ClaimTypes.Role)?.Value;

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }


            return RedirectToAction("Index", "Dashboard");
        }
        
        public IActionResult SignUp()
        {
            return View();
        }
    }

}
