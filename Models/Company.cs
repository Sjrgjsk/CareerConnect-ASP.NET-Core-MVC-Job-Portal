using System.ComponentModel.DataAnnotations;

namespace CareerConnect.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(150)]
        public string CompanyName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Phone]
        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        [StringLength(255)]
        public string? Website { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        

        [StringLength(500)]
        public string? Description { get; set; }

        public virtual ICollection<Job>? Jobs { get; set; }
    }
}