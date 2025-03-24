using System;
using System.IO;
using System.Linq;

namespace Cloudenum.FileDetective.Abstracts
{
    /// <summary>
    /// Common abstract class for detector that relies on file signature sequence(s)
    /// </summary>
    public abstract class AbstractSignatureSequenceDetector : IFileDetector
    {
        /// <inheritdoc/>
        public abstract string Description { get; }

        /// <inheritdoc/>
        public abstract string MimeType { get; }

        /// <summary>
        /// File signature sequences
        /// </summary>
        public abstract FileSignatureSequence[] SignatureSequences { get; }

        /// <inheritdoc/>
        public virtual bool Matches(Stream stream)
        {
            foreach (var sequence in SignatureSequences)
            {
                if (sequence.Length > 0 && stream.Length >= sequence.Length)
                {
                    foreach (var signature in sequence)
                    {
                        var bufferSize = signature.MagicBytes.Length;
                        byte[] buffer = new byte[bufferSize];
                        stream.Position = signature.Offset;
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);

                        if (bytesRead == 0)
                        {
                            return false;
                        }

                        if (bytesRead < bufferSize)
                        {
                            Array.Resize(ref buffer, bytesRead);
                        }

                        if (!buffer.SequenceEqual(signature.MagicBytes))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }
            return false;
        }
    }
}
