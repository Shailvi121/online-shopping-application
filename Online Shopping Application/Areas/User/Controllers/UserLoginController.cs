using Microsoft.AspNetCore.Mvc;

namespace Online_Shopping_Application.Area.Admin.Controllers
{
    [Area("User")]
    public class UserLoginController : Controller
    {
      //  [Route("admin-sign-in")]
        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
    }
}
