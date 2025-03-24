using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    /// <summary>
    /// Detects JPEG 2000 images
    /// </summary>
    public class Jpeg2000Detector : AbstractSignatureDetector
    {
        /// <inheritdoc/>
        public override string Description { get; } = "JPEG 2000";

        /// <inheritdoc/>
        public override string MimeType { get; } = "image/jp2";

        /// <inheritdoc/>
        public override FileSignature[] Signatures { get; } = new FileSignature[]
        {
            new FileSignature()
            {
                Offset = 0,
                MagicBytes = new byte[] { 0x00, 0x00, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20, 0x0D, 0x0A, 0x87, 0x0A },
            },
            new FileSignature() {
                Offset = 0,
                MagicBytes = new byte[] { 0xFF, 0x4F, 0xFF, 0x51 },
            }
        };
    }
}
