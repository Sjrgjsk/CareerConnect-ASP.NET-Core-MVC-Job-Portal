using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace CareerConnect.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }


        [StringLength(255)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? State { get; set; }

        [StringLength(10)]
        public string? Pincode { get; set; }

        [StringLength(100)]
        public string? Qualification { get; set; }

        [StringLength(500)]
        public string? Skills { get; set; }

        [StringLength(255)]
        public string? ProfileImage { get; set; }

        [StringLength(255)]
        public string? ResumeFile { get; set; }

        public bool IsRecruiter { get; set; } = false;

        public bool IsAdmin { get; set; } = false;

        public virtual ICollection<Application>? Applications { get; set; }

        public virtual ICollection<SavedJob>? SavedJobs { get; set; }

        public virtual ICollection<Feedback>? Feedbacks { get; set; }
    }
}