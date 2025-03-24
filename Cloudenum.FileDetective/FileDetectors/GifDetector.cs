using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    /// <summary>
    /// Detects GIF files
    /// </summary>
    public class GifDetector : AbstractSignatureDetector
    {
        /// <inheritdoc/>
        public override string Description { get; } = "Graphics Interchange Format";

        /// <inheritdoc/>
        public override string MimeType { get; } = "image/gif";

        /// <inheritdoc/>
        public override FileSignature[] Signatures { get; } =
        {
            new FileSignature
            {
                Offset = 0,
                MagicBytes = new byte[] { 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 }
            },
            new FileSignature
            {
                Offset = 0,
                MagicBytes = new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 }
            }
        };
    }
}
