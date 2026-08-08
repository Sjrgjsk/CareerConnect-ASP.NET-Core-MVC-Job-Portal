using CareerConnect.Models;

namespace CareerConnect.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<List<Feedback>> GetAllFeedbacksAsync();
        Task<Feedback?> GetFeedbackByIdAsync(int id);
        Task CreateFeedbackAsync(Feedback feedback);
        Task UpdateFeedbackAsync(Feedback feedback);
        Task DeleteFeedbackAsync(int id);
    }
}