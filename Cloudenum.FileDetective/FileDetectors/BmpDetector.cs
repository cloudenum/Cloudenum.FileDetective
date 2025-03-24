using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    /// <summary>
    /// Detects Bitmap files
    /// </summary>
    public class BmpDetector : AbstractSignatureDetector
    {
        /// <inheritdoc/>
        public override string Description { get; } = "Bitmap";

        /// <inheritdoc/>
        public override string MimeType { get; } = "image/bmp";

        /// <inheritdoc/>
        public override FileSignature[] Signatures { get; } =
        {
            new FileSignature
            {
                Offset = 0,
                MagicBytes = new byte[] { 0x42, 0x4D }
            }
        };
    }
}
