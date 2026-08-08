using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerConnect.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        [StringLength(150)]
        public string Subject { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }

        public DateTime FeedbackDate { get; set; } = DateTime.Now;

        // Foreign Key
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }
    }
}