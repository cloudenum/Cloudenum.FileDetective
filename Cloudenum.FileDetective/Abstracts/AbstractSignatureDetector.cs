using System;
using System.IO;
using System.Linq;

namespace Cloudenum.FileDetective.Abstracts
{
    /// <summary>
    /// Common abstract class for detectors that relies on file signature(s)
    /// </summary>
    public abstract class AbstractSignatureDetector : IFileDetector
    {
        /// <inheritdoc/>
        public abstract string Description { get; }

        /// <inheritdoc/>
        public abstract string MimeType { get; }

        /// <summary>
        /// Magic bytes of the file signature
        /// </summary>
        public abstract FileSignature[] Signatures { get; }

        /// <inheritdoc/>
        public virtual bool Matches(Stream stream)
        {
            foreach (var signature in Signatures)
            {
                if (stream.Length >= signature.Offset + signature.MagicBytes.Length)
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

                    if (buffer.SequenceEqual(signature.MagicBytes))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
