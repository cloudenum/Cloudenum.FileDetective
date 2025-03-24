using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    /// <summary>
    /// Detects Microsoft PowerPoint 97-2003 Presentation files
    /// </summary>
    public class PptDetector : AbstractCfbfDetector
    {
        /// <inheritdoc/>
        public override string Description { get; } = "Microsoft PowerPoint 97-2003 Presentation";

        /// <inheritdoc/>
        public override string MimeType { get; } = "application/vnd.ms-powerpoint";

        /// <inheritdoc/>
        protected override CompoundFileContent[] Contents { get; } =
        {
            new CompoundFileContent { Type = CompoundFileContentType.Stream, Name = "PowerPoint Document" }
        };
    }
}
