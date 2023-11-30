using Microsoft.AspNetCore.Mvc;

namespace Online_Shopping_Application.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Blog()
        {
            return View();
        }
    }
}
