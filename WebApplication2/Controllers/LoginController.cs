using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Data;
using System.IO;
using System.Runtime.InteropServices;

namespace WebApplication2.Controllers
{
    public class LoginController : Controller
    {
        private readonly WebApplication2Context _context;

        public LoginController(WebApplication2Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Email, Password")] Models.User _user)
        {
            await _context.SaveChangesAsync();

            var user = _context.User.FirstOrDefault(x => x.Email == _user.Email);

            if (user != null && VerifyPassword(_user.Password, user.Password))
            {
                // Redirect to the home page or any other authenticated page
                Console.WriteLine("Login successful");
                System.IO.File.WriteAllText(@"C:\\Кодинг\\Cloud\\WebApplication2\\WebApplication2\temp.txt", string.Empty);
                System.IO.File.WriteAllText(@"C:\\Кодинг\\Cloud\\WebApplication2\\WebApplication2\temp.txt", user.Id.ToString());
                return RedirectToAction("Index", "Files");
            }
            else
            {
                Console.WriteLine("Login Failed");
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }
        }


        private bool VerifyPassword(string? enteredPassword, string? storedPassword)
        {
            // Implement your password verification logic here
            // You may use a secure password hashing algorithm like bcrypt or Argon2
            // Compare the enteredPassword with the storedPassword after applying the hashing algorithm
            // Return true if they match, false otherwise

            // Example using plain text comparison (not recommended for production)
            return enteredPassword == storedPassword;
        }
    }
}
