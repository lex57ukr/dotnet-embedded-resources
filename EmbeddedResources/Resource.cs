using System;
using System.IO;
using System.Reflection;
using static EmbeddedResources.Hashing;


namespace EmbeddedResources
{
    /// <summary>
    /// Embedded resource.
    /// </summary>
    public class Resource
    {
        private Assembly Assembly { get; }
        private string Name { get; }

        /// <summary>
        /// Initializes a resource name from the source assembly
        /// and its fully qualified name.
        /// </summary>
        /// <param name="assembly">The source assembly.</param>
        /// <param name="name">The fully qualified resource name.</param>
        /// <exception name="ArgumentNullException"/>
        /// <exception name="ArgumentException"/>
        public Resource(Assembly assembly, string name)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    message:   "Valid resource name is expected.",
                    paramName: nameof(name)
                );
            }

            var info = assembly.GetManifestResourceInfo(name);
            if (info == null)
            {
                throw new ArgumentException(
                    message:   $"Resource '{name}' not found in '{assembly.FullName}'.",
                    paramName: nameof(name)
                );
            }

            this.Assembly = assembly;
            this.Name     = name;
        }

        /// <summary>
        /// Get the fully qualified resource name.
        /// </summary>
        /// <returns>Resource name.</returns>
        public override string ToString()
            => $"Resource '{this.Name}' in '{this.Assembly.FullName}'";

        /// <summary>
        /// Open the resource stream.
        /// </summary>
        /// <remarks>
        /// ATTENTION! The calling party must dispose of the stream.
        /// </remarks>
        /// <returns>The resource stream.</returns>
        public Stream OpenStream()
            => this.Assembly.GetManifestResourceStream(this.Name);

        /// <summary>
        /// Get the hashcode of this instance.
        /// </summary>
        /// <returns>Hash code.</returns>
        public override int GetHashCode()
            => HashMany(this.Assembly, this.Name);

        /// <summary>
        /// Compare this instance to another object for equality.
        /// </summary>
        /// <param name="obj">Another object.</param>
        /// <returns><b>true</b> if the object is equal.</returns>
        public override bool Equals(object obj)
        {
            if ( ! (obj is Resource))
            {
                return false;
            }

            return ReferenceEquals(this, obj) || PropsEqual((Resource) obj);
        }

        private bool PropsEqual(Resource other)
            => this.Assembly == other.Assembly && this.Name == other.Name;
    }
}
