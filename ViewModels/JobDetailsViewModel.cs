using CareerConnect.Models;

namespace CareerConnect.ViewModels
{
    public class JobDetailsViewModel
    {
        // Job Information
        public Job? Job { get; set; }

        // Company Information
        public Company? Company { get; set; }

        // Category Information
        public Category? Category { get; set; }

        // Number of applications for this job
        public int TotalApplications { get; set; }

        // Is this job saved by current user?
        public bool IsSaved { get; set; }

        // Has the current user already applied?
        public bool HasApplied { get; set; }
    }
}