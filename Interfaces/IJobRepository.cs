using CareerConnect.Models;

namespace CareerConnect.Interfaces
{
    public interface IJobRepository
    {
        Task<List<Job>> GetAllJobsAsync();

        Task<Job?> GetJobByIdAsync(int id);

        Task CreateJobAsync(Job job);

        Task UpdateJobAsync(Job job);

        Task DeleteJobAsync(int id);
    }
}