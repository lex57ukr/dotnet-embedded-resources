using Xunit;
using System;
using System.Reflection;
using EmbeddedResources;


namespace EmbeddedResourcesTests
{
    public class ResourceTests
    {
        private const string ExistingResourceName
            = "EmbeddedResourcesTests.Resources.Sample.txt";

        private static Assembly Assembly
            => Assembly.GetExecutingAssembly();

        [Fact]
        public void Ctor_NullAssembly_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                "assembly",
                () => new Resource(null, ExistingResourceName)
            );
        }

        [Fact]
        public void Ctor_MissingResource_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(
                "name",
                () => new Resource("*")
            );
        }

        public void Ctor_NullName_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                "name",
                () => new Resource(null)
            );
        }

        [Theory]
        [InlineData("")]
        [InlineData("\t")]
        [InlineData("    ")]
        public void Ctor_BlankName_ThrowsArgumentException(string name)
        {
            Assert.Throws<ArgumentException>(
                "name",
                () => new Resource(name)
            );
        }

        [Fact]
        public void Ctor_FromExecutingAssembly_AreEqual()
        {
            var x = new Resource(Assembly, ExistingResourceName);
            var y = new Resource(ExistingResourceName);

            Assert.Equal(x, y);
        }

        [Fact]
        public void Ctor_FromExecutingAssembly_ProduceSameHashCode()
        {
            var x = new Resource(Assembly, ExistingResourceName).GetHashCode();
            var y = new Resource(ExistingResourceName).GetHashCode();

            Assert.Equal(x, y);
        }

        [Fact]
        public void Equals_AnotherObject_ReturnFalse()
        {
            var result = new Resource(ExistingResourceName).Equals(new object());
            Assert.False(result);
        }

        [Fact]
        public void Equals_Null_ReturnFalse()
        {
            var result = new Resource(ExistingResourceName).Equals(null);
            Assert.False(result);
        }

        [Fact]
        public void Equals_Self_ReturnTrue()
        {
            var x = new Resource(ExistingResourceName);

            // ReSharper disable once EqualExpressionComparison
            var result = x.Equals(x);
            Assert.True(result);
        }

        [Fact]
        public void ToString_ContainsAssemblyFullName()
        {
            var result = new Resource(ExistingResourceName).ToString();
            Assert.Contains(Assembly.FullName, result);
        }

        [Fact]
        public void ToString_ContainsResourceName()
        {
            var result = new Resource(ExistingResourceName).ToString();
            Assert.Contains(ExistingResourceName, result);
        }
    }
}
