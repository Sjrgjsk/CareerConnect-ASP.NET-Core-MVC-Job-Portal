using CareerConnect.Models;
using CareerConnect.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CareerConnect.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // ==========================
        // REGISTER (GET)
        // ==========================
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // ==========================
        // REGISTER (POST)
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Only Recruiter and JobSeeker can register
            if (model.Role != "Recruiter" && model.Role != "JobSeeker")
            {
                ModelState.AddModelError("", "Invalid role selected.");
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);

                TempData["Success"] = "Registration successful. Please login.";

                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        // ==========================
        // LOGIN (GET)
        // ==========================
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // ==========================
        // LOGIN (POST)
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                        return RedirectToAction("Dashboard", "Admin");

                    if (await _userManager.IsInRoleAsync(user, "Recruiter"))
                        return RedirectToAction("Index", "Company");

                    if (await _userManager.IsInRoleAsync(user, "JobSeeker"))
                        return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid Email or Password");

            return View(model);
        }

        // ==========================
        // LOGOUT
        // ==========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        // ==========================
        // ACCESS DENIED
        // ==========================
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}