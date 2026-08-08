using CareerConnect.Interfaces;
using CareerConnect.Models;

namespace CareerConnect.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<List<Job>> GetAllJobsAsync()
        {
            return await _jobRepository.GetAllJobsAsync();
        }

        public async Task<Job?> GetJobByIdAsync(int id)
        {
            return await _jobRepository.GetJobByIdAsync(id);
        }

        public async Task CreateJobAsync(Job job)
        {
            await _jobRepository.CreateJobAsync(job);
        }

        public async Task UpdateJobAsync(Job job)
        {
            await _jobRepository.UpdateJobAsync(job);
        }

        public async Task DeleteJobAsync(int id)
        {
            await _jobRepository.DeleteJobAsync(id);
        }
    }
}