using CareerConnect.Models;

namespace CareerConnect.Interfaces
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAllCompaniesAsync();

        Task<Company?> GetCompanyByIdAsync(int id);

        Task CreateCompanyAsync(Company company);

        Task UpdateCompanyAsync(Company company);

        Task DeleteCompanyAsync(int id);
    }
}