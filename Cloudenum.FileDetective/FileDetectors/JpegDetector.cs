using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    public class JpegDetector : AbstractSignatureDetector
    {
        public override string Description { get; } = "JPEG";

        public override string MimeType { get; } = "image/jpeg";

        public override FileSignature[] Signatures { get; } = new FileSignature[]
        {
            new FileSignature() {
                Offset = 0,
                MagicBytes = new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
            },
            new FileSignature() {
                Offset = 0,
                MagicBytes = new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
            },
            new FileSignature() {
                Offset = 0,
                MagicBytes = new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
            },
            new FileSignature() {
                Offset = 0,
                MagicBytes = new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
            },
            new FileSignature() {
                Offset = 0,
                MagicBytes = new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
            },
            new FileSignature() {
                Offset = 0,
                MagicBytes = new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
            },
            new FileSignature() {
                Offset = 0,
                MagicBytes = new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
            },
            new FileSignature() {
                Offset = 0,
                MagicBytes = new byte[] { 0xFF, 0xD8, 0xFF, 0xFE, 0x00 },
            },
        };
    }
}
