using CareerConnect.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareerConnect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Latest Jobs
            var latestJobs = await _context.Jobs
                .Include(j => j.Company)
                .Include(j => j.Category)
                .OrderByDescending(j => j.PostedDate)
                .Take(3)
                .ToListAsync();

            // Categories with Jobs
            ViewBag.Categories = await _context.Categories
                .Include(c => c.Jobs)
                .ToListAsync();

            return View(latestJobs);
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}