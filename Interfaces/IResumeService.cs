using CareerConnect.Models;

namespace CareerConnect.Interfaces
{
    public interface IResumeService
    {
        Task<List<Resume>> GetAllResumesAsync();

        Task<Resume?> GetResumeByIdAsync(int id);

        Task CreateResumeAsync(Resume resume);

        Task UpdateResumeAsync(Resume resume);

        Task DeleteResumeAsync(int id);
    }
}