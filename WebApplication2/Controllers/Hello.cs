using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace WebApplication2.Controllers
{
    public class Hello : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome(int id, string message)
        {
            return View();//HtmlEncoder.Default.Encode($"Welcome user {id}, {message}");
        }

        //public IActionResult ()
        //{

        //}
    }
}
