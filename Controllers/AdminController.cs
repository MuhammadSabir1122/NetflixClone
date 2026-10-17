using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetflixClone.Data;
using NetflixClone.Models;

namespace NetflixClone.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }


        // =========================
        // CHECK ADMIN LOGIN
        // =========================

        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.GetInt32("AdminId") != null;
        }


        // =========================
        // ADMIN DASHBOARD
        // =========================

        // GET: /Admin
        public IActionResult Index()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("AdminLogin", "Account");
            }

            return View();
        }


        // =========================
        // CATEGORY CRUD
        // =========================

        // GET: /Admin/Categories
        public async Task<IActionResult> Categories()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("AdminLogin", "Account");
            }

            var categories = await _context.Categories
                .ToListAsync();

            return View(categories);
        }


        // GET: /Admin/CreateCategory
        public IActionResult CreateCategory()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("AdminLogin", "Account");
            }

            return View();
        }


        // POST: /Admin/CreateCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("AdminLogin", "Account");
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Categories));
            }

            return View(category);
        }


        // GET: /Admin/EditCategory/5
        public async Task<IActionResult> EditCategory(int? id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("AdminLogin", "Account");
            }

            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        // POST: /Admin/EditCategory/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(
            int id,
            Category category)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("AdminLogin", "Account");
            }

            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Categories));
            }

            return View(category);
        }


        // POST: /Admin/DeleteCategory/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("AdminLogin", "Account");
            }

            var category = await _context.Categories
                .FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Categories));
        }


        // =========================
        // MOVIE CRUD
        // =========================

        // GET: /Admin/Movies
        public async Task<IActionResult> Movies()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("AdminLogin", "Account");
            }

            var movies = await _context.Movies
                .Include(m => m.Category)
                .ToListAsync();

            return View(movies);
        }


        // GET: /Admin/CreateMovie
        public async Task<IActionResult> CreateMovie()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("AdminLogin", "Account");
            }

            ViewBag.Categories = await _context.Categories
                .ToListAsync();

            return View();
        }


        // POST: /Admin/CreateMovie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMovie(Movie movie)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("AdminLogin", "Account");
            }

            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Movies));
            }

            ViewBag.Categories = await _context.Categories
                .ToListAsync();

            return View(movie);
        }


        // GET: /Admin/EditMovie/5
        public async Task<IActionResult> EditMovie(int? id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("AdminLogin", "Account");
            }

            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _context.Categories
                .ToListAsync();

            return View(movie);
        }


        // POST: /Admin/EditMovie/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMovie(
            int id,
            Movie movie)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("AdminLogin", "Account");
            }

            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Movies.Update(movie);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Movies));
            }

            ViewBag.Categories = await _context.Categories
                .ToListAsync();

            return View(movie);
        }


        // POST: /Admin/DeleteMovie/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("AdminLogin", "Account");
            }

            var movie = await _context.Movies
                .FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Movies));
        }
    }
}