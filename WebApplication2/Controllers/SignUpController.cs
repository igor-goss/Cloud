using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;

namespace WebApplication2.Controllers
{
    public class SignUpController : Controller
    {
        private readonly WebApplication2Context _context;

        public SignUpController(WebApplication2Context context)
        {
            _context = context;            
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Id, FirstName, LastName, Email, Password")] Models.User user)
        {
            if(ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }
    }
}
