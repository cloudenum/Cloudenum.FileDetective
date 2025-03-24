using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    /// <summary>
    /// Detects Tagged Image File Format (TIFF) files
    /// </summary>
    public class TiffDetector : AbstractSignatureDetector
    {
        /// <inheritdoc/>
        public override string Description { get; } = "Tagged Image File Format";

        /// <inheritdoc/>
        public override string MimeType { get; } = "image/tiff";

        /// <inheritdoc/>
        public override FileSignature[] Signatures { get; } =
        {
            new FileSignature
            {
                Offset = 0,
                MagicBytes = new byte[] { 0x49, 0x49, 0x2A, 0x00 }
            },
            new FileSignature
            {
                Offset = 0,
                MagicBytes = new byte[] { 0x4D, 0x4D, 0x00, 0x2A }
            },
            new FileSignature
            {
                Offset = 0,
                MagicBytes = new byte[] { 0x49, 0x49, 0x2B, 0x00 }
            },
            new FileSignature
            {
                Offset = 0,
                MagicBytes = new byte[] { 0x4D, 0x4D, 0x00, 0x2B }
            },
        };
    }
}
