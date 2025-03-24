using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    /// <summary>
    /// Detects Microsoft Excel 97-2003 Worksheet files
    /// </summary>
    public class XlsDetector : AbstractCfbfDetector
    {
        /// <inheritdoc/>
        public override string Description { get; } = "Microsoft Excel 97-2003 Worksheet";

        /// <inheritdoc/>
        public override string MimeType { get; } = "application/vnd.ms-excel";

        /// <inheritdoc/>
        protected override CompoundFileContent[] Contents { get; } =
        {
            new CompoundFileContent { Type = CompoundFileContentType.Stream, Name = "Workbook" }
        };
    }
}
