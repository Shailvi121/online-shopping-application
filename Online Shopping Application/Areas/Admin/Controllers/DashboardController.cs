using Microsoft.AspNetCore.Mvc;

namespace Online_Shopping_Application.Areas.Admin.Controllers
{
     [Area("Admin")]
   // [Authorize(Roles = Constants.Roles.Admin)]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
