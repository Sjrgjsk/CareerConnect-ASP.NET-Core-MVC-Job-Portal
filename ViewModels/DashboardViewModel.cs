using CareerConnect.Models;

namespace CareerConnect.ViewModels
{
    public class DashboardViewModel
    {
        // Dashboard Counts
        public int TotalUsers { get; set; }

        public int TotalCompanies { get; set; }

        public int TotalCategories { get; set; }

        public int TotalJobs { get; set; }

        public int TotalApplications { get; set; }

        public int TotalResumes { get; set; }

        public int TotalFeedbacks { get; set; }

        // Recent Data
        public List<Job>? RecentJobs { get; set; }

        public List<Company>? RecentCompanies { get; set; }

        public List<Application>? RecentApplications { get; set; }
    }
}