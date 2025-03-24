using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    /// <summary>
    /// Detects Microsoft Excel Open XML Documents
    /// </summary>
    public class XlsxDetector : AbstractZipBasedDetector
    {
        /// <inheritdoc/>
        public override string Description { get; } = "Microsoft Excel Open XML Document";

        /// <inheritdoc/>
        public override string MimeType { get; } = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        /// <inheritdoc/>
        protected override string[] ZipEntries { get; } = {
            "[Content_Types].xml",
            "xl/workbook.xml"
        };
    }
}
