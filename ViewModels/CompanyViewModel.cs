using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CareerConnect.ViewModels
{
    public class CompanyViewModel
    {
        public int CompanyId { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Website")]
        public string? Website { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? Location { get; set; }

        public string? Logo { get; set; }

        [Display(Name = "Company Logo")]
        public IFormFile? LogoFile { get; set; }

        public int TotalJobs { get; set; }
    }
}