using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    /// <summary>
    /// Detects ZIP archives
    /// </summary>
    public class ZipArchiveDetector : AbstractSignatureDetector
    {
        /// <inheritdoc/>
        public override string Description { get; } = "ZIP archive";

        /// <inheritdoc/>
        public override string MimeType { get; } = "application/zip";

        /// <inheritdoc/>
        public override FileSignature[] Signatures { get; } = {
            new FileSignature()
            {
                Offset = 0,
                MagicBytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 }
            },
            new FileSignature()
            {
                Offset = 0,
                MagicBytes = new byte[] { 0x50, 0x4B, 0x05, 0x06 }
            },
            new FileSignature()
            {
                Offset = 0,
                MagicBytes = new byte[] { 0x50, 0x4B, 0x07, 0x08 }
            }
        };
    }
}
