using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    /// <summary>
    /// Detects Microsoft PowerPoint Open XML Presentation files
    /// </summary>
    public class PptxDetector : AbstractZipBasedDetector
    {
        /// <inheritdoc/>
        public override string Description { get; } = "Microsoft PowerPoint Open XML Presentation";

        /// <inheritdoc/>
        public override string MimeType { get; } = "application/vnd.openxmlformats-officedocument.presentationml.presentation";

        /// <inheritdoc/>
        protected override string[] ZipEntries { get; } = {
            "[Content_Types].xml",
            "ppt/presentation.xml"
        };
    }
}
