using Microsoft.AspNetCore.Http;

namespace CareerConnect.Helpers
{
    public static class FileUploadHelper
    {
        public static async Task<string?> UploadFileAsync(
            IFormFile? file,
            string folderPath)
        {
            if (file == null || file.Length == 0)
                return null;

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public static void DeleteFile(string? fileName, string folderPath)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            string filePath = Path.Combine(folderPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}