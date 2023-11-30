using Microsoft.AspNetCore.Mvc;

namespace Online_Shopping_Application.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Cart()
        {
            return View();
        }
    }
}
