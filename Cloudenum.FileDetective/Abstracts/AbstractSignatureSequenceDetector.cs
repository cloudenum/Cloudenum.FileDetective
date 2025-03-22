using System.Linq;

namespace Cloudenum.FileDetective.Abstracts
{
    /// <summary>
    /// Common abstract class for detector that relies on file signature sequence(s)
    /// </summary>
    public abstract class AbstractSignatureSequenceDetector : IFileDetector
    {
        public abstract string Description { get; }

        public abstract string MimeType { get; }

        /// <summary>
        /// File signature sequences
        /// </summary>
        public abstract FileSignatureSequence[] SignatureSequences { get; }

        public virtual bool Matches(byte[] fileBytes)
        {
            if (fileBytes == null || fileBytes.Length == 0)
            {
                return false;
            }

            if (SignatureSequences != null)
            {
                foreach (var sequence in SignatureSequences)
                {
                    if (sequence.Length > 0 && fileBytes.Length >= sequence.Length)
                    {
                        bool sequenceMatch = true;
                        foreach (var signature in sequence)
                        {
                            byte[] currentSignature = fileBytes.Skip(signature.Offset).Take(signature.MagicBytes.Length).ToArray();
                            if (currentSignature.SequenceEqual(signature.MagicBytes))
                            {
                                sequenceMatch &= true;
                            }
                            else
                            {
                                sequenceMatch &= false;
                                break;
                            }
                        }

                        if (sequenceMatch)
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
