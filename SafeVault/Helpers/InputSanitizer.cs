using System.Text.RegularExpressions;

namespace SafeVault.Helpers
{
    public static class InputSanitizer
    {
        public static string Sanitize(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            input = Regex.Replace(input, "<.*?>", string.Empty); // Remove HTML tags
            input = Regex.Replace(input, @"[^\w\s.]", string.Empty); // Remove special characters
            return input.Trim();
        }
    }
}
