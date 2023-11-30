using Microsoft.AspNetCore.Mvc;

namespace Online_Shopping_Application.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Products()
        {
            return View();
        }
    }
}
