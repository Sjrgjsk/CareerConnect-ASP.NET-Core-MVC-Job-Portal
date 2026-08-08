using Microsoft.AspNetCore.Http;

namespace CareerConnect.Helpers
{
    public static class ImageHelper
    {
        private static readonly string[] AllowedExtensions =
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".gif",
            ".webp"
        };

        public static bool IsValidImage(IFormFile? file)
        {
            if (file == null)
                return false;

            string extension = Path.GetExtension(file.FileName).ToLower();

            return AllowedExtensions.Contains(extension);
        }

        public static bool IsValidImageSize(IFormFile? file, int maxSizeInMB = 2)
        {
            if (file == null)
                return false;

            return file.Length <= maxSizeInMB * 1024 * 1024;
        }

        public static string GenerateImageName(IFormFile file)
        {
            return Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        }
    }
}