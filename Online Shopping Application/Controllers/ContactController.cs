using Microsoft.AspNetCore.Mvc;

namespace Online_Shopping_Application.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
    }
}
