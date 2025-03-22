using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    public class PdfDetector : AbstractSignatureDetector
    {
        public override string Description { get; } = "Portable Document Format";

        public override string MimeType { get; } = "application/pdf";

        public override FileSignature[] Signatures { get; } = new[]
        {
            new FileSignature()
            {
                Offset = 0,
                MagicBytes = new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D }
            },
        };
    }
}
