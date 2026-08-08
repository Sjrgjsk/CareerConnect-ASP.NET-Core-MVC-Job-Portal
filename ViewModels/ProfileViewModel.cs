using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CareerConnect.ViewModels
{
    public class ProfileViewModel
    {
        public int ProfileId { get; set; }

        public string? UserId { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Mobile Number")]
        public string? MobileNumber { get; set; }

        public string? Qualification { get; set; }

        public string? Skills { get; set; }

        public string? Experience { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        [Display(Name = "About Me")]
        public string? AboutMe { get; set; }

        public string? ProfileImage { get; set; }

        [Display(Name = "Profile Photo")]
        public IFormFile? ProfileImageFile { get; set; }
    }
}