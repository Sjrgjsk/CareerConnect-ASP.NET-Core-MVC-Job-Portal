using CareerConnect.Models;

namespace CareerConnect.Interfaces
{
    public interface IProfileRepository
    {
        Task<List<Profile>> GetAllProfilesAsync();
        Task<Profile?> GetProfileByIdAsync(int id);
        Task CreateProfileAsync(Profile profile);
        Task UpdateProfileAsync(Profile profile);
        Task DeleteProfileAsync(int id);
    }
}