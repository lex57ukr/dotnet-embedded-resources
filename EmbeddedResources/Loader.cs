using System.IO;
using System.Reflection;
using static EmbeddedResources.IO;
using static EmbeddedResources.Vocabluary;


namespace EmbeddedResources
{
    /// <summary>
    /// Embedded resource loader.
    /// </summary>
    public class Loader
    {
        private readonly Assembly _source;

        /// <summary>
        /// Loader for assembly.
        /// </summary>
        /// <param name="source">The source assembly.</param>
        public Loader(Assembly source)
            => _source = source;

        /// <summary>
        /// Compare this instance to another <see cref="obj"/> for equality.
        /// </summary>
        /// <param name="obj">Another object.</param>
        /// <returns><b>true</b> if instances are equal.</returns>
        public override bool Equals(object obj)
        {
            if ( ! (obj is Loader))
            {
                return false;
            }

            return ReferenceEquals(this, obj) || PropEquals((Loader) obj);
        }

        /// <summary>
        /// Get the hashcode.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
            => _source.GetHashCode();

        /// <summary>
        /// Get a friendly string representation of this instance.
        /// </summary>
        /// <returns>Full name of this resource loader.</returns>
        public override string ToString()
            => $"Embedded Resources Loader: {_source.FullName}";

        /// <summary>
        /// Get the contents of the embedded resource by its namespace
        /// qualified resource name.
        /// </summary>
        /// <param name="resourceName">Namespace qualified resource name.</param>
        /// <returns>
        /// String contents of the resource or <b>null</b> if the resource
        /// was not found.
        /// </returns>
        public string GetString(string resourceName)
            => Using(() => GetStream(resourceName), ReadStringToEnd);

        /// <summary>
        /// Get the formatted contents of the embedded resource by its namespace
        /// qualified resource name.
        /// </summary>
        /// <param name="resourceName">Namespace qualified resource name.</param>
        /// <param name="arg">Format argument.</param>
        /// <returns>
        /// String contents of the resource or <b>null</b> if the resource
        /// was not found.
        /// </returns>
        public string GetString(string resourceName, params object[] arg)
            => Format(GetString(resourceName), arg);

        private Stream GetStream(string name)
            => _source.GetManifestResourceStream(name);

        private bool PropEquals(Loader other)
            => _source == other._source;
    }
}
