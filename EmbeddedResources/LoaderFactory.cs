namespace EmbeddedResources
{
    /// <summary>
    /// Creates new loaders.
    /// </summary>
    public static class LoaderFactory
    {
        /// <summary>
        /// Creates a typed loader.
        /// </summary>
        /// <returns>A new loader.</returns>
        public static Loader New<T>()
            => new Loader(typeof(T).Assembly);
    }
}
