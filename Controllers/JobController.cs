using CareerConnect.Data;
using CareerConnect.Interfaces;
using CareerConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CareerConnect.Controllers
{

    [Authorize]
    public class JobController : Controller
    {
        private readonly IJobService _jobService;
        private readonly ApplicationDbContext _context;

        public JobController(IJobService jobService, ApplicationDbContext context)
        {
            _jobService = jobService;
            _context = context;
        }

        // =======================
        // INDEX + SEARCH + FILTER
        // =======================
        public async Task<IActionResult> Index(
     string searchString,
     string location,
     int? categoryId,
     string category,
     string jobType)
        {
            var jobs = _context.Jobs
                .Include(j => j.Company)
                .Include(j => j.Category)
                .AsQueryable();

            // Search by Job Title
            if (!string.IsNullOrEmpty(searchString))
            {
                jobs = jobs.Where(j =>
                    j.JobTitle.Contains(searchString));
            }

            // Location Filter
            if (!string.IsNullOrEmpty(location))
            {
                jobs = jobs.Where(j =>
                    j.Location.Contains(location));
            }

            // Category Filter
            if (categoryId.HasValue)
            {
                jobs = jobs.Where(j =>
                    j.CategoryId == categoryId.Value);
            }

            // Category Name Filter (From Home Page)
            if (!string.IsNullOrEmpty(category))
            {
                jobs = jobs.Where(j =>
                    j.Category.CategoryName == category);
            }

            // Job Type Filter
            if (!string.IsNullOrEmpty(jobType))
            {
                jobs = jobs.Where(j =>
                    j.JobType == jobType);
            }

            // Category Dropdown
            ViewBag.CategoryList = new SelectList(
                _context.Categories.ToList(),
                "CategoryId",
                "CategoryName",
                categoryId);

            ViewBag.SearchString = searchString;
            ViewBag.Location = location;
            ViewBag.JobType = jobType;

            return View(await jobs.ToListAsync());
        }
        // =======================
        // DETAILS
        // =======================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var job = await _jobService.GetJobByIdAsync(id.Value);

            if (job == null)
                return NotFound();

            return View(job);
        }

        // =======================
        // CREATE (GET)
        // =======================
        public IActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(_context.Companies, "CompanyId", "CompanyName");
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName");

            return View();
        }

        // =======================
        // CREATE (POST)
        // =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Job job)
        {
            if (ModelState.IsValid)
            {
                await _jobService.CreateJobAsync(job);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CompanyId = new SelectList(_context.Companies, "CompanyId", "CompanyName", job.CompanyId);
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", job.CategoryId);

            return View(job);
        }

        // =======================
        // EDIT (GET)
        // =======================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var job = await _jobService.GetJobByIdAsync(id.Value);

            if (job == null)
                return NotFound();

            ViewBag.CompanyId = new SelectList(_context.Companies, "CompanyId", "CompanyName", job.CompanyId);
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", job.CategoryId);

            return View(job);
        }

        // =======================
        // EDIT (POST)
        // =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Job job)
        {
            if (id != job.JobId)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _jobService.UpdateJobAsync(job);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CompanyId = new SelectList(_context.Companies, "CompanyId", "CompanyName", job.CompanyId);
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", job.CategoryId);

            return View(job);
        }

        // =======================
        // DELETE (GET)
        // =======================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var job = await _jobService.GetJobByIdAsync(id.Value);

            if (job == null)
                return NotFound();

            return View(job);
        }

        // =======================
        // DELETE (POST)
        // =======================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _jobService.DeleteJobAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}