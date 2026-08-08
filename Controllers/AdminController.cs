using CareerConnect.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareerConnect.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            // ==========================
            // Dashboard Cards
            // ==========================

            ViewBag.TotalUsers = _context.Users.Count();

            ViewBag.TotalCompanies = _context.Companies.Count();

            ViewBag.TotalCategories = _context.Categories.Count();

            ViewBag.TotalJobs = _context.Jobs.Count();

            ViewBag.TotalApplications = _context.Applications.Count();

            ViewBag.TotalResumes = _context.Resumes.Count();

            ViewBag.TotalFeedbacks = _context.Feedbacks.Count();

            // ==========================
            // Application Status
            // ==========================

            ViewBag.Pending =
                _context.Applications.Count(a => a.Status == "Pending");

            ViewBag.Accepted =
                _context.Applications.Count(a => a.Status == "Accepted");

            ViewBag.Rejected =
                _context.Applications.Count(a => a.Status == "Rejected");

            // ==========================
            // Recent Jobs
            // ==========================

            ViewBag.RecentJobs = _context.Jobs
                .Include(j => j.Company)
                .OrderByDescending(j => j.PostedDate)
                .Take(5)
                .ToList();

            // ==========================
            // Recent Applications
            // ==========================

            ViewBag.RecentApplications = _context.Applications
                .Include(a => a.Job)
                .Include(a => a.User)
                .OrderByDescending(a => a.ApplicationDate)
                .Take(5)
                .ToList();

            // ==========================
            // Jobs By Category Chart
            // ==========================

            ViewBag.CategoryNames = _context.Categories
                .Select(c => c.CategoryName)
                .ToList();

            ViewBag.CategoryCounts = _context.Categories
                .Select(c =>
                    _context.Jobs.Count(j => j.CategoryId == c.CategoryId))
                .ToList();

            return View();
        }
    }
}