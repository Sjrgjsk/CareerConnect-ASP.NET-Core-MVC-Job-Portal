using CareerConnect.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareerConnect.Controllers
{
    [Authorize(Roles = "Recruiter")]
    public class RecruiterDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecruiterDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalJobs = _context.Jobs.Count();

            ViewBag.TotalApplications = _context.Applications.Count();

            ViewBag.PendingApplications =
                _context.Applications.Count(a => a.Status == "Pending");

            ViewBag.AcceptedApplications =
                _context.Applications.Count(a => a.Status == "Accepted");

            ViewBag.RejectedApplications =
                _context.Applications.Count(a => a.Status == "Rejected");

            // Latest 5 Jobs
            ViewBag.RecentJobs = _context.Jobs
                .OrderByDescending(j => j.PostedDate)
                .Take(5)
                .ToList();

            return View();
        }
    }
}