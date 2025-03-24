using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    /// <summary>
    /// Detects WebP files
    /// </summary>
    public class WebpDetector : AbstractSignatureSequenceDetector
    {
        /// <inheritdoc/>
        public override string Description { get; } = "WebP";

        /// <inheritdoc/>
        public override string MimeType { get; } = "image/webp";

        /// <inheritdoc/>
        public override FileSignatureSequence[] SignatureSequences { get; } =
        {
            new FileSignatureSequence()
            {
                new FileSignature
                {
                    Offset = 0,
                    MagicBytes = new byte[] { 0x52, 0x49, 0x46, 0x46 }
                },
                new FileSignature
                {
                    Offset = 8,
                    MagicBytes = new byte[] { 0x57, 0x45, 0x42, 0x50 }
                }
            }
        };
    }
}
