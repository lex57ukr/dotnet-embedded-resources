using Xunit;
using System.Reflection;
using EmbeddedResources;


namespace EmbeddedResourcesTests
{
    public class LoaderTests
    {
        private const string ExistingResourceName
            = "EmbeddedResourcesTests.Resources.Sample.txt";

        private const string ExistingResourceNameFormat2
            = "EmbeddedResourcesTests.Resources.Format2.txt";

        private const string MissingResourceName
            = ExistingResourceName + ".missing";

        private static Assembly Assembly
            => Assembly.GetExecutingAssembly();

        [Fact]
        public void Ctor_FromSameAssembly_AreEqual()
        {
            var x = NewLoader();
            var y = NewLoader();

            Assert.Equal(x, y);
        }

        [Fact]
        public void Ctor_FromSameAssembly_ProduceSameHashCode()
        {
            var x = NewLoader().GetHashCode();
            var y = NewLoader().GetHashCode();

            Assert.Equal(x, y);
        }

        [Fact]
        public void Equals_AnotherObject_ReturnFalse()
        {
            var result = NewLoader().Equals(new object());
            Assert.False(result);
        }

        [Fact]
        public void Equals_Null_ReturnFalse()
        {
            var result = NewLoader().Equals(null);
            Assert.False(result);
        }

        [Fact]
        public void Equals_Self_ReturnTrue()
        {
            var loader = NewLoader();

            // ReSharper disable once EqualExpressionComparison
            var result = loader.Equals(loader);
            Assert.True(result);
        }

        [Fact]
        public void GetString_ValidResource_ReturnContent()
        {
            const string expected = "Sample file as embedded resource.";

            var result = NewLoader().GetString(ExistingResourceName);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetString_MissingResource_ReturnNull()
        {
            var result = NewLoader().GetString(MissingResourceName);
            Assert.Null(result);
        }

        [Fact]
        public void GetString_Formatted_ValidResource_ReturnFormattedContent()
        {
            const string expected = "Formatted file as embedded resource: XYZ (123)";

            var result = NewLoader()
                .GetString(ExistingResourceNameFormat2, "XYZ", 123);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetString_Formatted_MissingResource_ReturnNull()
        {
            var result = NewLoader()
                .GetString(MissingResourceName, "XYZ", 123);

            Assert.Null(result);
        }

        [Fact]
        public void ToString_ContainsAssemblyFullName()
        {
            var result = NewLoader().ToString();
            Assert.Contains(Assembly.FullName, result);
        }

        private static Loader NewLoader()
            => new Loader(Assembly);
    }
}
