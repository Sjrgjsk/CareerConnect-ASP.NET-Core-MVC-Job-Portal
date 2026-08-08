using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerConnect.Models
{
    public class Resume
    {
        [Key]
        public int ResumeId { get; set; }

        [Required]
        [StringLength(150)]
        public string ResumeTitle { get; set; }

        [StringLength(255)]
        public string? ResumeFile { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.Now;

        // Foreign Key
       
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }
    }
}