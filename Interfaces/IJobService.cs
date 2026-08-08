using CareerConnect.Models;

namespace CareerConnect.Interfaces
{
    public interface IJobService
    {
        Task<List<Job>> GetAllJobsAsync();

        Task<Job?> GetJobByIdAsync(int id);

        Task CreateJobAsync(Job job);

        Task UpdateJobAsync(Job job);

        Task DeleteJobAsync(int id);
    }
}