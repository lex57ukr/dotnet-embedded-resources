using Xunit;
using System.Reflection;
using EmbeddedResources;
using static EmbeddedResources.LoaderFactory;


namespace EmbeddedResourcesTests
{
    public class LoaderFactoryTests
    {
        [Fact]
        public void New_UsesAssemblyOfType()
        {
            var x = New<LoaderFactoryTests>();
            var y = new Loader(Assembly.GetExecutingAssembly());

            Assert.Equal(x, y);
        }
    }
}
