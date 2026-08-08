using Microsoft.AspNetCore.Http;

namespace CareerConnect.Helpers
{
    public static class ResumeHelper
    {
        private static readonly string[] AllowedExtensions =
        {
            ".pdf",
            ".doc",
            ".docx"
        };

        public static bool IsValidResume(IFormFile? file)
        {
            if (file == null)
                return false;

            string extension = Path.GetExtension(file.FileName).ToLower();

            return AllowedExtensions.Contains(extension);
        }

        public static bool IsValidResumeSize(IFormFile? file, int maxSizeInMB = 5)
        {
            if (file == null)
                return false;

            return file.Length <= maxSizeInMB * 1024 * 1024;
        }

        public static string GenerateResumeName(IFormFile file)
        {
            return Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        }
    }
}