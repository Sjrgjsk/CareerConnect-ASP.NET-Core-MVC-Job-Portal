using CareerConnect.Interfaces;
using CareerConnect.Models;

namespace CareerConnect.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<List<Company>> GetAllCompaniesAsync()
        {
            return await _companyRepository.GetAllCompaniesAsync();
        }

        public async Task<Company?> GetCompanyByIdAsync(int id)
        {
            return await _companyRepository.GetCompanyByIdAsync(id);
        }

        public async Task CreateCompanyAsync(Company company)
        {
            await _companyRepository.CreateCompanyAsync(company);
        }

        public async Task UpdateCompanyAsync(Company company)
        {
            await _companyRepository.UpdateCompanyAsync(company);
        }

        public async Task DeleteCompanyAsync(int id)
        {
            await _companyRepository.DeleteCompanyAsync(id);
        }
    }
}