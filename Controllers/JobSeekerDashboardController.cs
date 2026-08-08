using CareerConnect.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CareerConnect.Controllers
{
    [Authorize(Roles = "JobSeeker")]
    public class JobSeekerDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobSeekerDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewBag.ProfileCount =
                _context.Profiles.Count(p => p.UserId == userId);

            ViewBag.ResumeCount =
                _context.Resumes.Count(r => r.UserId == userId);

            ViewBag.ApplicationCount =
                _context.Applications.Count(a => a.UserId == userId);

            ViewBag.SavedJobCount =
                _context.SavedJobs.Count(s => s.UserId == userId);

            return View();
        }
    }
}