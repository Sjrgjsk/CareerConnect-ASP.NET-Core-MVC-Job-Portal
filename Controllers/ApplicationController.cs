using CareerConnect.Data;
using CareerConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CareerConnect.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // INDEX
        // =========================
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Index()
        {
            // Get logged-in user's ID
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Show only this user's applications
            var applications = await _context.Applications
                .Include(a => a.Job)
                .Include(a => a.User)
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return View(applications);
        }
        // =====================================
        // VIEW APPLICANTS FOR A JOB
        // =====================================
        [Authorize(Roles = "Recruiter,Admin")]
        public async Task<IActionResult> Applicants(int jobId)
        {
            var applicants = await _context.Applications
                .Include(a => a.User)
                .Include(a => a.Job)
                .Where(a => a.JobId == jobId)
                .ToListAsync();

            foreach (var applicant in applicants)
            {
                applicant.User.ResumeFile = _context.Resumes
                    .Where(r => r.UserId == applicant.UserId)
                    .Select(r => r.ResumeFile)
                    .FirstOrDefault();
            }

            return View(applicants);
        }
        // =====================================
        // ACCEPT APPLICATION
        // =====================================
        [Authorize(Roles = "Recruiter,Admin")]
        public async Task<IActionResult> Accept(int id)
        {
            var application = await _context.Applications.FindAsync(id);

            if (application == null)
                return NotFound();

            application.Status = "Accepted";

            _context.Update(application);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Applicants), new { jobId = application.JobId });
        }
        // =====================================
        // REJECT APPLICATION
        // =====================================
        [Authorize(Roles = "Recruiter,Admin")]
        public async Task<IActionResult> Reject(int id)
        {
            var application = await _context.Applications.FindAsync(id);

            if (application == null)
                return NotFound();

            application.Status = "Rejected";

            _context.Update(application);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Applicants), new { jobId = application.JobId });
        }
        // =========================
        // DETAILS
        // =========================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var application = await _context.Applications
                .Include(a => a.Job)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.ApplicationId == id);

            if (application == null)
                return NotFound();

            return View(application);
        }

        // =========================
        // CREATE (GET)
        // =========================
        [Authorize(Roles = "JobSeeker")]
        public IActionResult Create(int? jobId)
        {
            ViewBag.JobId = new SelectList(_context.Jobs, "JobId", "JobTitle");
          

            return View();
        }

        // =========================
        // CREATE (POST)
        // =========================
        [Authorize(Roles = "JobSeeker")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Application application)
        {
            // Automatically assign logged-in user
            application.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            // Set default values
            application.ApplicationDate = DateTime.Now;
            application.Status = "Pending";

            // Check if the user has already applied for this job
            bool alreadyApplied = await _context.Applications.AnyAsync(a =>
                a.JobId == application.JobId &&
                a.UserId == application.UserId);

            if (alreadyApplied)
            {
                ModelState.AddModelError("", "You have already applied for this job.");
            }

            application.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            // Remove validation error for UserId because we set it in code
            ModelState.Remove("UserId");

            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"{state.Key} : {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                _context.Applications.Add(application);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.JobId = new SelectList(_context.Jobs, "JobId", "JobTitle", application.JobId);

            return View(application);
        }

        // =========================
        // DELETE (GET)
        // =========================
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var application = await _context.Applications
                .Include(a => a.Job)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.ApplicationId == id);

            if (application == null)
                return NotFound();

            return View(application);
        }

        // =========================
        // DELETE (POST)
        // =========================
        [Authorize(Roles = "JobSeeker")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Applications.FindAsync(id);

            if (application != null)
            {
                _context.Applications.Remove(application);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}