using CareerConnect.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CareerConnect.Interfaces
{
    public interface IProfileService
    {
        Task<List<Profile>> GetAllProfilesAsync();
        Task<Profile?> GetProfileByIdAsync(int id);
        Task CreateProfileAsync(Profile profile);
        Task UpdateProfileAsync(Profile profile);
        Task DeleteProfileAsync(int id);
    }
}