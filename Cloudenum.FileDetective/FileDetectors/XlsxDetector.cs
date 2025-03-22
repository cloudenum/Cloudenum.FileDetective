using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    public class XlsxDetector : AbstractZipBasedDetector
    {
        public override string Description { get; } = "Microsoft Excel Open XML Document";

        public override string MimeType { get; } = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        protected override string[] ContentsToLookFor { get; } = {
            "[Content_Types].xml",
            "xl/workbook.xml"
        };
    }
}
