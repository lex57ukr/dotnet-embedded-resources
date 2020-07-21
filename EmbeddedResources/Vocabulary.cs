using System;


namespace EmbeddedResources
{
    /// <summary>
    /// Functional vocabulary with language constructs.
    /// </summary>
    internal static class Vocabulary
    {
        /// <summary>
        /// Execute a <paramref name="func"/> inside a protected using clause.
        /// </summary>
        /// <param name="init">Initializes the target disposable object.</param>
        /// <param name="func">A handler to be executed on the target object.</param>
        /// <typeparam name="T">The target type.</typeparam>
        /// <typeparam name="TResult">Type of the return value.</typeparam>
        /// <returns>Value of <paramref name="func"/> invocation.</returns>
        public static TResult Using<T, TResult>(Func<T> init, Func<T, TResult> func)
            where T : IDisposable
        {
            using var target = init();
            return func(target);
        }
    }
}
