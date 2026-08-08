using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerConnect.Models
{
    public class Profile
    {
        [Key]
        public int ProfileId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Qualification { get; set; }

        [StringLength(100)]
        public string? Skills { get; set; }

        [StringLength(100)]
        public string? Experience { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? State { get; set; }

        [StringLength(15)]
        public string? MobileNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? AboutMe { get; set; }

        public string? ProfileImage { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}