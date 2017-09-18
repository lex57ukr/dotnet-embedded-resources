using Xunit;
using EmbeddedResources;
using static EmbeddedResources.IO;

namespace EmbeddedResourcesTests
{
    public class IOTests
    {
        private const string FormatResourceName
            = "EmbeddedResourcesTests.Resources.Sample.format.txt";

        private const string MultilineResourceName
            = "EmbeddedResourcesTests.Resources.Sample.multiline.txt";

        [Fact]
        public void GetTextContents_Multiline_ReturnAllContent()
        {
            var x      = new Resource(MultilineResourceName);
            var result = GetTextContents(x);

            const string expected = "Line 1\nLine 2\nLine 3\n";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetTextContents_Formatted_ReturnFormattedContent()
        {
            var x      = new Resource(FormatResourceName);
            var result = GetTextContents(x, "XYZ", 123);

            const string expected = "Placeholders:\n* XYZ\n* 123\n";
            Assert.Equal(expected, result);
        }
    }
}
