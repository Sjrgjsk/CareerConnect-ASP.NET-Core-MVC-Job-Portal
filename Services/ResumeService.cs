using CareerConnect.Interfaces;
using CareerConnect.Models;

namespace CareerConnect.Services
{
    public class ResumeService : IResumeService
    {
        private readonly IResumeRepository _resumeRepository;

        public ResumeService(IResumeRepository resumeRepository)
        {
            _resumeRepository = resumeRepository;
        }

        public async Task<List<Resume>> GetAllResumesAsync()
        {
            return await _resumeRepository.GetAllResumesAsync();
        }

        public async Task<Resume?> GetResumeByIdAsync(int id)
        {
            return await _resumeRepository.GetResumeByIdAsync(id);
        }

        public async Task CreateResumeAsync(Resume resume)
        {
            await _resumeRepository.CreateResumeAsync(resume);
        }

        public async Task UpdateResumeAsync(Resume resume)
        {
            await _resumeRepository.UpdateResumeAsync(resume);
        }

        public async Task DeleteResumeAsync(int id)
        {
            await _resumeRepository.DeleteResumeAsync(id);
        }
    }
}