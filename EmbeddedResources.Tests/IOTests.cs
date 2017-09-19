using Xunit;
using static EmbeddedResources.IO;
using static EmbeddedResources.Tests.Resources.Factory;


namespace EmbeddedResources.Tests
{
    public class IOTests
    {
        [Fact]
        public void GetTextContents_Multiline_ReturnAllContent()
        {
            var x      = NewResource(Samples.Multiline);
            var result = GetTextContents(x);

            var expected = "Line 1\nLine 2\nLine 3\n";
            Assert.Equal(expected, result, ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void GetTextContents_Formatted_ReturnFormattedContent()
        {
            var x      = NewResource(Samples.Format);
            var result = GetTextContents(x, "XYZ", 123);

            const string expected = "Placeholders:\n* XYZ\n* 123\n";
            Assert.Equal(expected, result, ignoreLineEndingDifferences: true);
        }
    }
}
