using NUnit.Framework;
using SafeVault.Helpers;

namespace SafeVault.Tests
{
    [TestFixture]
    public class InputSanitizerTests
    {
        [Test]
        public void Sanitize_ShouldRemoveHtmlTags()
        {
            string input = "<script>alert('XSS');</script>Test";
            string sanitized = InputSanitizer.Sanitize(input);
            Assert.That(sanitized, Is.EqualTo("alertXSSTest"));
        }

        [Test]
        public void Sanitize_ShouldRemoveSpecialCharacters()
        {
            string input = "Hello@!#$%^&*()World";
            string sanitized = InputSanitizer.Sanitize(input);
            Assert.That(sanitized, Is.EqualTo("HelloWorld"));
        }

        [Test]
        public void Sanitize_ShouldTrimSpaces()
        {
            string input = "   TrimMe   ";
            string sanitized = InputSanitizer.Sanitize(input);
            Assert.That(sanitized, Is.EqualTo("TrimMe"));
        }

        [Test]
        public void Sanitize_ShouldHandleEmptyString()
        {
            string input = "";
            string sanitized = InputSanitizer.Sanitize(input);
            Assert.That(sanitized, Is.EqualTo(""));
        }
    }
}
