using System;
using System.IO;
using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    public class PlainTextDetector : AbstractTextDetector
    {
        public override string Description => "Plain text file";

        public override string MimeType => "text/plain";

        protected override string[] TextSignatures { get; } = null;

        public override bool Matches(Stream stream)
        {
            return TextDetectorHelper.IsText(stream);
        }
    }
}
