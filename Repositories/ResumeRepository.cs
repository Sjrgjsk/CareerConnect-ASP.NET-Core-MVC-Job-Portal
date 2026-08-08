using CareerConnect.Data;
using CareerConnect.Interfaces;
using CareerConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerConnect.Repositories
{
    public class ResumeRepository : IResumeRepository
    {
        private readonly ApplicationDbContext _context;

        public ResumeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Resume>> GetAllResumesAsync()
        {
            return await _context.Resumes.ToListAsync();
        }

        public async Task<Resume?> GetResumeByIdAsync(int id)
        {
            return await _context.Resumes.FindAsync(id);
        }

        public async Task CreateResumeAsync(Resume resume)
        {
            _context.Resumes.Add(resume);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateResumeAsync(Resume resume)
        {
            _context.Resumes.Update(resume);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteResumeAsync(int id)
        {
            var resume = await _context.Resumes.FindAsync(id);

            if (resume != null)
            {
                _context.Resumes.Remove(resume);
                await _context.SaveChangesAsync();
            }
        }
    }
}