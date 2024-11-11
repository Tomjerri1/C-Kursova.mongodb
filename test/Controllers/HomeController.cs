// Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Administrator()
        {
            return View();
        }
        public IActionResult Admin()
        {
            return View();
        }
    }
}
