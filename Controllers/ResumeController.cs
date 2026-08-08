using CareerConnect.Data;
using CareerConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace CareerConnect.Controllers
{
    [Authorize]
    public class ResumeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ResumeController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // INDEX
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Index()
        {
            var resumes = _context.Resumes.Include(r => r.User);
            return View(await resumes.ToListAsync());
        }

        // CREATE GET
        [Authorize(Roles = "JobSeeker")]
        public IActionResult Create()
        {
            return View(new Resume());
        }

        // CREATE POST
        [Authorize(Roles = "JobSeeker")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Resume resume, IFormFile ResumeUpload)
        {
            if (ModelState.IsValid)
            {
                // Get currently logged-in user's ID
                resume.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                if (ResumeUpload != null && ResumeUpload.Length > 0)
                {
                    // Allowed file extensions
                    string[] allowedExtensions = { ".pdf", ".doc", ".docx" };

                    string extension = Path.GetExtension(ResumeUpload.FileName).ToLower();

                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("", "Only PDF, DOC and DOCX files are allowed.");
                        return View(resume);
                    }
                    // Maximum file size = 5 MB
                    if (ResumeUpload.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("", "File size must not exceed 5 MB.");
                        return View(resume);
                    }

                    string folderPath = Path.Combine(_environment.WebRootPath, "uploads", "resumes");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ResumeUpload.FileName);

                    string filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ResumeUpload.CopyToAsync(stream);
                    }

                    resume.ResumeFile = "uploads/resumes/" + fileName;
                }

                _context.Resumes.Add(resume);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(resume);
        }

        // DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var resume = await _context.Resumes
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ResumeId == id);

            if (resume == null)
                return NotFound();

            return View(resume);
        }

        // DOWNLOAD RESUME
        public async Task<IActionResult> Download(int id)
        {
            var resume = await _context.Resumes.FindAsync(id);

            if (resume == null)
                return NotFound();

            var path = Path.Combine(_environment.WebRootPath, resume.ResumeFile);

            if (!System.IO.File.Exists(path))
                return NotFound();

            var bytes = await System.IO.File.ReadAllBytesAsync(path);

            var fileName = Path.GetFileName(path);

            return File(bytes, "application/octet-stream", fileName);
        }
        [Authorize(Roles = "Recruiter,Admin")]
        public async Task<IActionResult> ViewResume(string userId)
        {
            var resume = await _context.Resumes
                .FirstOrDefaultAsync(r => r.UserId == userId);

            if (resume == null)
                return NotFound();

            string path = Path.Combine(_environment.WebRootPath, resume.ResumeFile);

            if (!System.IO.File.Exists(path))
                return NotFound();

            return PhysicalFile(path, "application/pdf");
        }
        // DELETE GET
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var resume = await _context.Resumes
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ResumeId == id);

            if (resume == null)
                return NotFound();

            return View(resume);
        }

        // DELETE POST
        [Authorize(Roles = "JobSeeker")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resume = await _context.Resumes.FindAsync(id);

            if (resume != null)
            {
                _context.Resumes.Remove(resume);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}