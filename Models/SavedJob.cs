using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerConnect.Models
{
    public class SavedJob
    {
        [Key]
        public int SavedJobId { get; set; }

        public DateTime SavedDate { get; set; } = DateTime.Now;

        // Foreign Key for Job
        [Required]
        public int JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual Job? Job { get; set; }

        // Foreign Key for User
        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }
    }
}