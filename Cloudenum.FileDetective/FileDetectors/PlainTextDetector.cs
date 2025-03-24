using System.IO;
using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    /// <summary>
    /// Detects plain text files
    /// </summary>
    public class PlainTextDetector : AbstractTextDetector
    {
        /// <inheritdoc/>
        public override string Description => "Plain text file";
        
        /// <inheritdoc/>
        public override string MimeType => "text/plain";

        /// <inheritdoc/>
        protected override string[] TextSignatures { get; } = null;

        /// <inheritdoc/>
        public override bool Matches(Stream stream)
        {
            return TextDetectorHelper.IsText(stream);
        }
    }
}
