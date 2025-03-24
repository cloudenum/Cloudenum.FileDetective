using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Cloudenum.FileDetective.Abstracts
{
    /// <summary>
    /// Common abstract class for regex detectors
    /// </summary>
    public abstract class AbstractRegexDetector : IFileDetector
    {
        /// <inheritdoc/>
        public abstract string Description { get; }

        /// <inheritdoc/>
        public abstract string MimeType { get; }

        /// <summary>
        /// Regexes to match the file content
        /// </summary>
        /// <remarks>
        /// For performance and memory usage reasons, the regexes will only be applied to the first 4096 characters of the file
        /// </remarks>
        protected abstract Regex[] Regexes { get; }

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
                int bufferSize = 4096;
                char[] buffer = new char[bufferSize];
                int charsRead = reader.Read(buffer, 0, bufferSize);
                if (charsRead == 0) {
                    return false;
                }

                if (charsRead < bufferSize)
                {
                    Array.Resize(ref buffer, charsRead);
                }

                string text = new string(buffer);
                foreach (var regex in Regexes)
                {
                    if (regex.IsMatch(text))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
