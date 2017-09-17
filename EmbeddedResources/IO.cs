using System.IO;
using System.Text;
using static EmbeddedResources.Vocabluary;


namespace EmbeddedResources
{
    /// <summary>
    /// IO functions.
    /// </summary>
    internal static class IO
    {
        /// <summary>
        /// Read contents of the <paramref name="source"/> stream.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="detectEncodingFromByteOrderMarks">Look for byte order marks.</param>
        /// <param name="bufferSize">The minimum buffer size in bytes.</param>
        /// <param name="leaveOpen">Leave the <paramref name="source"/> stream open.</param>
        /// <returns>
        /// String contents of the <paramref name="source"/> stream or <b>null</b>
        /// if the stream is <b>null</b>.
        /// </returns>
        public static string ReadStringToEnd(
            Stream source,
            Encoding encoding,
            bool detectEncodingFromByteOrderMarks,
            int bufferSize,
            bool leaveOpen
        )
        {
            if (null == source)
            {
                return null;
            }

            return Using(
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

        /// <summary>
        /// Read contents of the <paramref name="source"/> stream. Leaves the
        /// stream open.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <returns>
        /// String contents of the <paramref name="source"/> stream or <b>null</b>
        /// if the stream is <b>null</b>.
        /// </returns>
        public static string ReadStringToEnd(Stream source)
            => ReadStringToEnd(
                source,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: true,
                bufferSize: ushort.MaxValue,
                leaveOpen: true
            );
    }
}
