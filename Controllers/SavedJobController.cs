using CareerConnect.Data;
using CareerConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CareerConnect.Controllers
{
    [Authorize(Roles = "JobSeeker")]
    public class SavedJobController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SavedJobController(ApplicationDbContext context)
        {
            _context = context;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var savedJobs = await _context.SavedJobs
                .Include(s => s.Job)
                .Include(s => s.User)
                .Where(s => s.UserId == userId)
                .ToListAsync();

            return View(savedJobs);
        }

        // CREATE GET
        public IActionResult Create()
        {
            ViewBag.JobId = new SelectList(_context.Jobs, "JobId", "JobTitle");
           

            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SavedJob savedJob)
        {
            // Automatically assign logged-in user
            savedJob.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            ModelState.Remove(nameof(SavedJob.UserId));

            // Set save date
            savedJob.SavedDate = DateTime.Now;

            // Check duplicate
            bool alreadySaved = await _context.SavedJobs.AnyAsync(s =>
                s.JobId == savedJob.JobId &&
                s.UserId == savedJob.UserId);

            if (alreadySaved)
            {
                ModelState.AddModelError("", "This job is already in your saved jobs.");
            }

            if (ModelState.IsValid)
            {
                _context.SavedJobs.Add(savedJob);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.JobId = new SelectList(_context.Jobs, "JobId", "JobTitle", savedJob.JobId);

            return View(savedJob);
        }

        // DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var savedJob = await _context.SavedJobs
                .Include(s => s.Job)
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.SavedJobId == id);

            if (savedJob == null)
                return NotFound();

            return View(savedJob);
        }

        // DELETE GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var savedJob = await _context.SavedJobs
                .Include(s => s.Job)
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.SavedJobId == id);

            if (savedJob == null)
                return NotFound();

            return View(savedJob);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var savedJob = await _context.SavedJobs.FindAsync(id);

            if (savedJob != null)
            {
                _context.SavedJobs.Remove(savedJob);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}