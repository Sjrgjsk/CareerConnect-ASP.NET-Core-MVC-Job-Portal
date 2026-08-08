using CareerConnect.Interfaces;
using CareerConnect.Models;

namespace CareerConnect.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<List<Application>> GetAllApplicationsAsync()
        {
            return await _applicationRepository.GetAllApplicationsAsync();
        }

        public async Task<Application?> GetApplicationByIdAsync(int id)
        {
            return await _applicationRepository.GetApplicationByIdAsync(id);
        }

        public async Task CreateApplicationAsync(Application application)
        {
            await _applicationRepository.CreateApplicationAsync(application);
        }

        public async Task UpdateApplicationAsync(Application application)
        {
            await _applicationRepository.UpdateApplicationAsync(application);
        }

        public async Task DeleteApplicationAsync(int id)
        {
            await _applicationRepository.DeleteApplicationAsync(id);
        }
    }
}