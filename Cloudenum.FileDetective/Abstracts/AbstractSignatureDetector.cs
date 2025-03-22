using System.Linq;

namespace Cloudenum.FileDetective.Abstracts
{
    /// <summary>
    /// Common abstract class for detectors that relies on file signature(s)
    /// </summary>
    public abstract class AbstractSignatureDetector : IFileDetector
    {
        public abstract string Description { get; }

        public abstract string MimeType { get; }

        /// <summary>
        /// Magic bytes of the file signature
        /// </summary>
        public abstract FileSignature[] Signatures { get; }

        public virtual bool Matches(byte[] fileBytes)
        {
            if (fileBytes == null || fileBytes.Length == 0)
            {
                return false;
            }

            if (Signatures != null)
            {
                foreach (var signature in Signatures)
                {
                    if (fileBytes.Length >= signature.Offset + signature.MagicBytes.Length)
                    {
                        byte[] currentFileSignature = fileBytes.Skip(signature.Offset).Take(signature.MagicBytes.Length).ToArray();
                        if (currentFileSignature.SequenceEqual(signature.MagicBytes))
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
