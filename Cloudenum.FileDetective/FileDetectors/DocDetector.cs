using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    /// <summary>
    /// Detects Microsoft Word 97-2003 Document files
    /// </summary>
    public class DocDetector : AbstractCfbfDetector
    {
        /// <inheritdoc/>
        public override string Description { get; } = "Microsoft Word 97-2003 Document";

        /// <inheritdoc/>
        public override string MimeType { get; } = "application/msword";

        /// <inheritdoc/>
        protected override CompoundFileContent[] Contents { get; } =
        {
            new CompoundFileContent { Type = CompoundFileContentType.Stream, Name = "WordDocument" }
        };
    }
}
