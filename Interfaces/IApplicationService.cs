using CareerConnect.Models;

namespace CareerConnect.Interfaces
{
    public interface IApplicationService
    {
        Task<List<Application>> GetAllApplicationsAsync();
        Task<Application?> GetApplicationByIdAsync(int id);
        Task CreateApplicationAsync(Application application);
        Task UpdateApplicationAsync(Application application);
        Task DeleteApplicationAsync(int id);
    }
}