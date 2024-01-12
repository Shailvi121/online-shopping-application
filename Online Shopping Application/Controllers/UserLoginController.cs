namespace Online_Shopping_Application.Controllers
{
    public class UserLoginController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly HttpAPIWrapper _apiWrapper;

        public UserLoginController(IConfiguration configuration, HttpAPIWrapper apiWrapper)
        {
            _configuration = configuration;
            _apiWrapper = apiWrapper;
        }

        public IActionResult Login()
            {
            return View();
        }
        [HttpPost]
       
        public async Task<IActionResult> LoginIn(string username, string password)
        {
            var endpoint = Constants.APIEndpoints.Login;
            var content = new LoginAPIModel
            {
                Username = username,
                Password = password
            };

            var response = await _apiWrapper.PostAsync<TokenResponse, LoginAPIModel>(endpoint, content);

            if (response.IsSuccess)
            {
                var tokenData = response.data;

                // Parse the token
                var tokenHandler = new JwtSecurityTokenHandler();

                // Validate the token
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration.GetValue<string>("JWT:Issuer"),
                    ValidAudience = _configuration.GetValue<string>("JWT:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")))
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(tokenData.Token, tokenValidationParameters, out validatedToken);

               
                var userId = principal.FindFirst(ClaimTypes.Name)?.Value;
                var userRole = principal.FindFirst(ClaimTypes.Role)?.Value;
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                

                return RedirectToAction("Index", "Dashboard");
            }
            else
            {

                ViewBag.ErrorMessage = "Invalid username or password";
                return View("Login");
            }
        }


       
        public IActionResult Registration()
        {
            return View();
        }
    }
}
