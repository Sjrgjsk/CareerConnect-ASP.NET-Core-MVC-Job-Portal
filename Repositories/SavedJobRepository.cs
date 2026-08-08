using CareerConnect.Data;
using CareerConnect.Interfaces;
using CareerConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerConnect.Repositories
{
    public class SavedJobRepository : ISavedJobRepository
    {
        private readonly ApplicationDbContext _context;

        public SavedJobRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SavedJob>> GetAllSavedJobsAsync()
        {
            return await _context.SavedJobs
                .Include(s => s.Job)
                .ToListAsync();
        }

        public async Task<SavedJob?> GetSavedJobByIdAsync(int id)
        {
            return await _context.SavedJobs
                .Include(s => s.Job)
                .FirstOrDefaultAsync(s => s.SavedJobId == id);
        }

        public async Task CreateSavedJobAsync(SavedJob savedJob)
        {
            _context.SavedJobs.Add(savedJob);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSavedJobAsync(SavedJob savedJob)
        {
            _context.SavedJobs.Update(savedJob);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSavedJobAsync(int id)
        {
            var savedJob = await _context.SavedJobs.FindAsync(id);

            if (savedJob != null)
            {
                _context.SavedJobs.Remove(savedJob);
                await _context.SaveChangesAsync();
            }
        }
    }
}