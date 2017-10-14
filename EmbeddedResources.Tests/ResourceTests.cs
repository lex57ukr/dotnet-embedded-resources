using Xunit;
using System;
using static EmbeddedResources.Tests.Resources.Factory;


namespace EmbeddedResources.Tests
{
    public class ResourceTests
    {
        [Fact]
        public void Ctor_NullAssembly_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                "assembly",
                () => new Resource(null, ResourceName())
            );
        }

        [Fact]
        public void Ctor_MissingResource_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(
                "name",
                () => new Resource(MyAssembly, "*")
            );
        }

        public void Ctor_NullName_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                "name",
                () => new Resource(MyAssembly, null)
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
                () => new Resource(MyAssembly, name)
            );
        }

        [Fact]
        public void Ctor_FromExecutingAssembly_AreEqual()
        {
            var x = new Resource(MyAssembly, ResourceName());
            var y = new Resource(MyAssembly, ResourceName());

            Assert.Equal(x, y);
        }

        [Fact]
        public void Ctor_FromExecutingAssembly_ProduceSameHashCode()
        {
            var x = new Resource(MyAssembly, ResourceName()).GetHashCode();
            var y = new Resource(MyAssembly, ResourceName()).GetHashCode();

            Assert.Equal(x, y);
        }

        [Fact]
        public void Equals_AnotherObject_ReturnFalse()
        {
            var result = NewResource().Equals(new object());
            Assert.False(result);
        }

        [Fact]
        public void Equals_Null_ReturnFalse()
        {
            var result = NewResource().Equals(null);
            Assert.False(result);
        }

        [Fact]
        public void Equals_Self_ReturnTrue()
        {
            var x = NewResource();

            // ReSharper disable once EqualExpressionComparison
            var result = x.Equals(x);
            Assert.True(result);
        }

        [Fact]
        public void ToString_ContainsAssemblyFullName()
        {
            var result = NewResource().ToString();
            Assert.Contains(MyAssembly.FullName, result);
        }

        [Fact]
        public void ToString_ContainsResourceName()
        {
            var expected = ResourceName();
            var result   = NewResource().ToString();

            Assert.Contains(expected, result);
        }
    }
}
