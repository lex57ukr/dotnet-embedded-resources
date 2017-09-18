using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.Immutable;


namespace EmbeddedResources.Tests.Resources
{
    internal static class Factory
    {
        public static string Namespace
            => typeof(Factory).Namespace;

        public static Assembly MyAssembly
            => typeof(Factory).Assembly;

        public enum Samples
        {
            Default,
            Format,
            Multiline,
        }

        static readonly IDictionary<Samples, string> NameByType =
            new Dictionary<Samples, string> {
                [Samples.Default]   = "Sample",
                [Samples.Multiline] = "Sample.multiline",
                [Samples.Format]    = "Sample.format",
            }.ToImmutableDictionary();

        public static string ResourceName(Samples sample = Samples.Default)
            => $"{Namespace}.{NameByType[sample]}.txt";

        public static Resource NewResource(Samples sample = Samples.Default)
            => new Resource(ResourceName(sample));
    }
}
