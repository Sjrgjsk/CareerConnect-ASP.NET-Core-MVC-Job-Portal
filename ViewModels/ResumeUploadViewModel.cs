using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CareerConnect.ViewModels
{
    public class ResumeUploadViewModel
    {
        public int ResumeId { get; set; }

        [Required(ErrorMessage = "Resume Title is required")]
        [Display(Name = "Resume Title")]
        public string ResumeTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please upload your resume")]
        [Display(Name = "Resume File")]
        public IFormFile? ResumeFile { get; set; }

        public string? ResumePath { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.Now;
    }
}