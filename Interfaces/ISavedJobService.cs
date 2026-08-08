using CareerConnect.Models;

namespace CareerConnect.Interfaces
{
    public interface ISavedJobService
    {
        Task<List<SavedJob>> GetAllSavedJobsAsync();
        Task<SavedJob?> GetSavedJobByIdAsync(int id);
        Task CreateSavedJobAsync(SavedJob savedJob);
        Task UpdateSavedJobAsync(SavedJob savedJob);
        Task DeleteSavedJobAsync(int id);
    }
}