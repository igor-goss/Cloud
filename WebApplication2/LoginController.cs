using Microsoft.AspNetCore.Mvc;

namespace WebApplication2
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
