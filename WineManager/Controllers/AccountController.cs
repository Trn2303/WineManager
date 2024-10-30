using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using WineManager.Models;

namespace YourNamespace.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public JsonResult Register(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid data." });

            if (_context.Users.Any(u => u.Username == model.Username || u.Email == model.Email))
                return Json(new { success = false, message = "Username or Email already exists." });

            var user = new User
            {
                Username = model.Username,
                PasswordHash = HashPassword(model.Password),
                Email = model.Email
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Json(new { success = true, message = "User registered successfully." });
        }

        [HttpPost]
        public JsonResult Login(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid data." });

            var user = _context.Users
                .SingleOrDefault(u => u.Username == model.Username && VerifyPassword(model.Password, u.PasswordHash));

            if (user != null)
                return Json(new { success = true, message = "Login successful." });

            return Json(new { success = false, message = "Invalid username or password." });
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
