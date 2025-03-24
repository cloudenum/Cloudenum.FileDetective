using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    /// <summary>
    /// Detects Microsoft Word Open XML Documents
    /// </summary>
    public class DocxDetector : AbstractZipBasedDetector
    {
        /// <inheritdoc/>
        public override string Description { get; } = "Microsoft Word Open XML Document";

        /// <inheritdoc/>
        public override string MimeType { get; } = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        /// <inheritdoc/>
        protected override string[] ZipEntries { get; } = {
            "[Content_Types].xml",
            "word/document.xml"
        };
    }
}
