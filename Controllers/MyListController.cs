using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetflixClone.Data;

namespace NetflixClone.Controllers
{
    public class MyListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyListController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: /MyList
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("ChooseRole", "Account");
            }

            var myList = await _context.MyLists
                .Include(m => m.Movie)
                .ThenInclude(m => m.Category)
                .Where(m => m.UserId == userId)
                .ToListAsync();

            return View(myList);
        }


        // POST: /MyList/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int movieId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("ChooseRole", "Account");
            }

            var movie = await _context.Movies
                .FindAsync(movieId);

            if (movie == null)
            {
                return NotFound();
            }


            // Check if movie already exists in user's list

            var alreadyExists = await _context.MyLists
                .AnyAsync(m =>
                    m.UserId == userId &&
                    m.MovieId == movieId);

            if (alreadyExists)
            {
                return RedirectToAction("Index", "Movies");
            }


            var myList = new Models.MyList
            {
                UserId = userId.Value,
                MovieId = movieId
            };

            _context.MyLists.Add(myList);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        // POST: /MyList/Remove/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("ChooseRole", "Account");
            }

            var item = await _context.MyLists
                .FirstOrDefaultAsync(m =>
                    m.Id == id &&
                    m.UserId == userId);

            if (item == null)
            {
                return NotFound();
            }

            _context.MyLists.Remove(item);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}