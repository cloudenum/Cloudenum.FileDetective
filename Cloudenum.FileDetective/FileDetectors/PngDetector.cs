using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    public class PngDetector : AbstractSignatureDetector
    {
        public override string Description { get; } = "Portable Network Graphics";

        public override string MimeType { get; } = "image/png";

        public override FileSignature[] Signatures { get; } = new FileSignature[]
        {
            new FileSignature()
            {
                Offset = 0,
                MagicBytes = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }
            },
        };
    }
}
