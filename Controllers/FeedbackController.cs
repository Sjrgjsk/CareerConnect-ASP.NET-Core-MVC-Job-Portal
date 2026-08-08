using CareerConnect.Data;
using CareerConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace CareerConnect.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FeedbackController(
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // INDEX
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var feedbacks = _context.Feedbacks.Include(f => f.User);
            return View(await feedbacks.ToListAsync());
        }

        // DETAILS
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var feedback = await _context.Feedbacks
                .Include(f => f.User)
                .FirstOrDefaultAsync(f => f.FeedbackId == id);

            if (feedback == null)
                return NotFound();

            return View(feedback);
        }

        // CREATE GET
        [Authorize(Roles = "JobSeeker,Recruiter")]
        public IActionResult Create()
        {
            
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Feedback feedback)
        {
            var user = await _userManager.GetUserAsync(User);

            feedback.UserId = user.Id;
            feedback.Name = user.FullName;
            feedback.Email = user.Email;
            feedback.FeedbackDate = DateTime.Now;

            ModelState.Remove("Name");
            ModelState.Remove("Email");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Feedback submitted successfully.";

                return RedirectToAction(nameof(Create));
            }

            return View(feedback);
        }

        // DELETE GET
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var feedback = await _context.Feedbacks
                .Include(f => f.User)
                .FirstOrDefaultAsync(f => f.FeedbackId == id);

            if (feedback == null)
                return NotFound();

            return View(feedback);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);

            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}