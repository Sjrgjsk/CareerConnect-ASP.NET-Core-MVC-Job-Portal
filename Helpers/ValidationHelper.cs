using System.Text.RegularExpressions;

namespace CareerConnect.Helpers
{
    public static class ValidationHelper
    {
        // Email Validation
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Mobile Number Validation (10 digits)
        public static bool IsValidMobile(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
                return false;

            return Regex.IsMatch(mobile, @"^[6-9]\d{9}$");
        }

        // File Extension Validation
        public static bool IsAllowedExtension(string fileName, string[] allowedExtensions)
        {
            string extension = Path.GetExtension(fileName).ToLower();

            return allowedExtensions.Contains(extension);
        }
    }
}