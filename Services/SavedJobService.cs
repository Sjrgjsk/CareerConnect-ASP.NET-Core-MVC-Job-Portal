using CareerConnect.Interfaces;
using CareerConnect.Models;

namespace CareerConnect.Services
{
    public class SavedJobService : ISavedJobService
    {
        private readonly ISavedJobRepository _savedJobRepository;

        public SavedJobService(ISavedJobRepository savedJobRepository)
        {
            _savedJobRepository = savedJobRepository;
        }

        public async Task<List<SavedJob>> GetAllSavedJobsAsync()
        {
            return await _savedJobRepository.GetAllSavedJobsAsync();
        }

        public async Task<SavedJob?> GetSavedJobByIdAsync(int id)
        {
            return await _savedJobRepository.GetSavedJobByIdAsync(id);
        }

        public async Task CreateSavedJobAsync(SavedJob savedJob)
        {
            await _savedJobRepository.CreateSavedJobAsync(savedJob);
        }

        public async Task UpdateSavedJobAsync(SavedJob savedJob)
        {
            await _savedJobRepository.UpdateSavedJobAsync(savedJob);
        }

        public async Task DeleteSavedJobAsync(int id)
        {
            await _savedJobRepository.DeleteSavedJobAsync(id);
        }
    }

}
