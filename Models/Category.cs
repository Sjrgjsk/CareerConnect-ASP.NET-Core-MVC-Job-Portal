using System.ComponentModel.DataAnnotations;

namespace CareerConnect.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public virtual ICollection<Job>? Jobs { get; set; }
    }
}