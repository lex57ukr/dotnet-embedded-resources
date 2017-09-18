using Xunit;
using EmbeddedResources;
using static EmbeddedResources.IO;

namespace EmbeddedResourcesTests
{
    public class IOTests
    {
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

    }
}
