using System.IO;
using System.Text;
using static EmbeddedResources.Vocabulary;


namespace EmbeddedResources
{
    /// <summary>
    /// IO functions.
    /// </summary>
    public static class IO
    {
        /// <summary>
        /// Get formatted text contents of the <paramref name="resource"/>.
        /// </summary>
        /// <param name="resource">The source.</param>
        /// <param name="arg">Format argument.</param>
        /// <returns>String contents of the <paramref name="resource"/>.</returns>
        public static string GetTextContents(
            Resource resource,
            params object[] arg
        ) => string.Format(GetTextContents(resource), arg);

        /// <summary>
        /// Get text contents of the <paramref name="resource"/>.
        /// </summary>
        /// <param name="resource">The source.</param>
        /// <returns>String contents of the <paramref name="resource"/>.</returns>
        public static string GetTextContents(Resource resource)
            => Using(
                () => resource.OpenStream(),
                rs => ReadStringToEnd(
                    rs,
                    encoding: Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: true,
                    bufferSize: ushort.MaxValue,
                    leaveOpen: true
                )
            );

        private static string ReadStringToEnd(
            Stream source,
            Encoding encoding,
            bool detectEncodingFromByteOrderMarks,
            int bufferSize,
            bool leaveOpen
        ) => Using(
            () => new StreamReader(
                source,
                encoding,
                detectEncodingFromByteOrderMarks,
                bufferSize,
                leaveOpen
            ),

            reader => reader.ReadToEnd()
        );
    }
}
