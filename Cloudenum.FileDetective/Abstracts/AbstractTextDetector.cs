using System.IO;
using System.Text;

namespace Cloudenum.FileDetective.Abstracts
{
    /// <summary>
    /// Common abstract class for text detectors
    /// </summary>
    public abstract class AbstractTextDetector : IFileDetector
    {
        /// <inheritdoc/>
        public abstract string Description { get; }

        /// <inheritdoc/>
        public abstract string MimeType { get; }

        /// <summary>
        /// Text signatures to look for in the file
        /// </summary>
        /// <remarks>
        /// The detector will look for these signatures in the file content line by line.
        /// So, each signature is expected to be in a separate line not spanning multiple lines.
        /// If you need to look for a signature that spans multiple lines, 
        /// you could use the <see cref="AbstractRegexDetector"/> or override the <see cref="Matches(Stream)"/> method.
        /// </remarks>
        protected abstract string[] TextSignatures { get; }

        /// <inheritdoc/>
        public virtual bool Matches(Stream stream)
        {
            if (!TextDetectorHelper.IsText(stream))
            {
                return false;
            }

            using (var reader = new StreamReader(
                stream,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: true,
                bufferSize: 4096,
                leaveOpen: true
            ))
            {
                stream.Position = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    foreach (var signature in TextSignatures)
                    {
                        if (line.Contains(signature))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
