using CareerConnect.Models;

namespace CareerConnect.ViewModels
{
    public class JobSearchViewModel
    {
        public string? Keyword { get; set; }

        public string? Location { get; set; }

        public int? CategoryId { get; set; }

        public int? CompanyId { get; set; }

        public string? Experience { get; set; }

        public IEnumerable<Job>? Jobs { get; set; }

        public IEnumerable<Category>? Categories { get; set; }

        public IEnumerable<Company>? Companies { get; set; }
    }
}