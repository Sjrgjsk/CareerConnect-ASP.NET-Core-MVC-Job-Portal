using CareerConnect.Data;
using CareerConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace CareerConnect.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProfileController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // ============================
        // INDEX
        // ============================
        public async Task<IActionResult> Index()
        {
            // Get logged-in user's ID
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Get only this user's profile
            var profile = await _context.Profiles
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
            {
                return RedirectToAction(nameof(Create));
            }

            return View(profile);
        }

        // ============================
        // DETAILS
        // ============================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var profile = await _context.Profiles.FindAsync(id);

            if (profile == null)
                return NotFound();

            return View(profile);
        }

        // ============================
        // CREATE (GET)
        // ============================
        public IActionResult Create()
        {
            // Get logged-in user's ID
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Check whether this user already has a profile
            bool profileExists = _context.Profiles.Any(p => p.UserId == userId);

            if (profileExists)
            {
                TempData["Message"] = "You have already created your profile.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // ============================
        // CREATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Profile profile)
        {
            // Assign UserId BEFORE validation
            profile.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                _context.Profiles.Add(profile);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(profile);
        }
        // ============================
        // EDIT (GET)
        // ============================
        public async Task<IActionResult> Edit(int? id)
        {

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profile = await _context.Profiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
            {
                return RedirectToAction(nameof(Create));
            }
            return View(profile);
        }

        // ============================
        // EDIT (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Profile profile, IFormFile? ProfilePhoto)
        {
            if (ModelState.IsValid)
            {
                if (ProfilePhoto != null && ProfilePhoto.Length > 0)
                {
                    string folder = Path.Combine(_environment.WebRootPath, "uploads", "profile");

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePhoto.FileName);

                    string filePath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ProfilePhoto.CopyToAsync(stream);
                    }

                    profile.ProfileImage = "uploads/profile/" + fileName;
                }

                _context.Update(profile);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(profile);
        }
        // ============================
        // DELETE (GET)
        // ============================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var profile = await _context.Profiles.FindAsync(id);

            if (profile == null)
                return NotFound();

            return View(profile);
        }

        // ============================
        // DELETE (POST)
        // ============================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);

            if (profile != null)
            {
                _context.Profiles.Remove(profile);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}