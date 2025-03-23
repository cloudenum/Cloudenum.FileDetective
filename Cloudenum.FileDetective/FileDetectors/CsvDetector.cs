using System.Text.RegularExpressions;
using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    public class CsvDetector : AbstractRegexDetector
    {
        public override string Description => "Comma-Separated Values";
        
        public override string MimeType => "text/csv";

        protected override Regex[] Regexes { get; } =
        {
            new Regex(@"^.+(,.*)+$", RegexOptions.Compiled | RegexOptions.Multiline),
            new Regex(@"^.+(;.*)+$", RegexOptions.Compiled | RegexOptions.Multiline),
            new Regex(@"^.+(\|.*)+$", RegexOptions.Compiled | RegexOptions.Multiline),
            new Regex(@"^.+(\t.*)+$", RegexOptions.Compiled | RegexOptions.Multiline),
        };
    }
}
