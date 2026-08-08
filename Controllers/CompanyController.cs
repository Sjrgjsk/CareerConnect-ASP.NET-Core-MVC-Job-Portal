using CareerConnect.Interfaces;
using CareerConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CareerConnect.Controllers
{
    [Authorize(Roles = "Recruiter,Admin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // =======================
        // INDEX
        // =======================
        public async Task<IActionResult> Index()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return View(companies);
        }

        // =======================
        // DETAILS
        // =======================
        public async Task<IActionResult> Details(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);

            if (company == null)
                return NotFound();

            return View(company);
        }

        // =======================
        // CREATE (GET)
        // =======================
        public IActionResult Create()
        {
            return View();
        }

        // =======================
        // CREATE (POST)
        // =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company)
        {
            if (ModelState.IsValid)
            {
                await _companyService.CreateCompanyAsync(company);
                return RedirectToAction(nameof(Index));
            }

            return View(company);
        }

        // =======================
        // EDIT (GET)
        // =======================
        public async Task<IActionResult> Edit(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);

            if (company == null)
                return NotFound();

            return View(company);
        }

        // =======================
        // EDIT (POST)
        // =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Company company)
        {
            if (id != company.CompanyId)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _companyService.UpdateCompanyAsync(company);
                return RedirectToAction(nameof(Index));
            }

            return View(company);
        }

        // =======================
        // DELETE (GET)
        // =======================
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);

            if (company == null)
                return NotFound();

            return View(company);
        }

        // =======================
        // DELETE (POST)
        // =======================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _companyService.DeleteCompanyAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}