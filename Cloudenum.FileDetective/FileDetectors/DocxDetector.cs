using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    public class DocxDetector : AbstractZipBasedDetector
    {
        public override string Description { get; } = "Microsoft Word Open XML Document";

        public override string MimeType { get; } = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        protected override string[] ContentsToLookFor { get; } = {
            "[Content_Types].xml",
            "word/document.xml"
        };
    }
}
