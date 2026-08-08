using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerConnect.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }

        [Required]
        [StringLength(150)]
        public string JobTitle { get; set; }

        [Required]
        [StringLength(100)]
        public string JobType { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime PostedDate { get; set; } = DateTime.Now;

        public DateTime LastDateToApply { get; set; }

        // Foreign Keys
        public int CompanyId { get; set; }

        public int CategoryId { get; set; }

        // Navigation Properties
        [ForeignKey("CompanyId")]
        public virtual Company? Company { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        public virtual ICollection<Application>? Applications { get; set; }

        public virtual ICollection<SavedJob>? SavedJobs { get; set; }
    }
}