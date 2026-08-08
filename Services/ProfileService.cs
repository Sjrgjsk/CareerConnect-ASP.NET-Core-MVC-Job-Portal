using CareerConnect.Interfaces;
using CareerConnect.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CareerConnect.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<List<Profile>> GetAllProfilesAsync()
        {
            return await _profileRepository.GetAllProfilesAsync();
        }

        public async Task<Profile?> GetProfileByIdAsync(int id)
        {
            return await _profileRepository.GetProfileByIdAsync(id);
        }

        public async Task CreateProfileAsync(Profile profile)
        {
            await _profileRepository.CreateProfileAsync(profile);
        }

        public async Task UpdateProfileAsync(Profile profile)
        {
            await _profileRepository.UpdateProfileAsync(profile);
        }

        public async Task DeleteProfileAsync(int id)
        {
            await _profileRepository.DeleteProfileAsync(id);
        }
    }
}