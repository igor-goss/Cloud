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
                System.IO.File.WriteAllText(@"C:\\Кодинг\\Cloud\\WebApplication2\\WebApplication2\temp.txt", string.Empty);
                System.IO.File.WriteAllText(@"C:\\Кодинг\\Cloud\\WebApplication2\\WebApplication2\temp.txt", user.Id.ToString());
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }
    }
}
