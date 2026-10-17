using Microsoft.AspNetCore.Mvc;
using NetflixClone.Data;
using NetflixClone.Models;

namespace NetflixClone.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }


        // =========================
        // CHOOSE ROLE
        // =========================

        // GET: /Account/ChooseRole
        public IActionResult ChooseRole()
        {
            return View();
        }


        // =========================
        // ADMIN LOGIN
        // =========================

        // GET: /Account/AdminLogin
        public IActionResult AdminLogin()
        {
            return View();
        }


        // POST: /Account/AdminLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdminLogin(string email, string password)
        {
            email = email.Trim().ToLower();

            var admin = _context.Admins
                .FirstOrDefault(a =>
                    a.Email == email &&
                    a.Password == password);

            if (admin == null)
            {
                ViewBag.Error = "Invalid admin email or password!";
                return View();
            }

            // Save admin session
            HttpContext.Session.SetInt32("AdminId", admin.Id);
            HttpContext.Session.SetString("AdminName", admin.FullName);

            // Open Admin Dashboard
            return RedirectToAction("Index", "Admin");
        }


        // =========================
        // USER REGISTER
        // =========================

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }


        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(
            string fullName,
            string email,
            string password)
        {
            fullName = fullName.Trim();
            email = email.Trim().ToLower();

            // Check existing email
            var existingUser = _context.Users
                .FirstOrDefault(u => u.Email == email);

            if (existingUser != null)
            {
                ViewBag.Error = "Email already exists!";
                return View();
            }

            // Create user
            var user = new User
            {
                FullName = fullName,
                Email = email,
                Password = password
            };

            _context.Users.Add(user);

            _context.SaveChanges();

            return RedirectToAction(nameof(Login));
        }


        // =========================
        // USER LOGIN
        // =========================

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }


        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(
            string email,
            string password)
        {
            email = email.Trim().ToLower();

            var user = _context.Users
                .FirstOrDefault(u =>
                    u.Email == email &&
                    u.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password!";
                return View();
            }

            // Save user session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.FullName);

            // Open User Home
            return RedirectToAction("Index", "Home");
        }


        // =========================
        // LOGOUT
        // =========================

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(nameof(ChooseRole));
        }
    }
}