using System.Text.RegularExpressions;
using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    /// <summary>
    /// Detects for Scalable Vector Graphics (SVG) files
    /// </summary>
    public class SvgDetector : AbstractRegexDetector
    {
        /// <inheritdoc/>
        public override string Description { get; } = "Scalable Vector Graphics (SVG)";

        /// <inheritdoc/>
        public override string MimeType { get; } = "image/svg+xml";

        /// <inheritdoc/>
        protected override Regex[] Regexes => new[]
        {
            new Regex(@"<svg[^>]+xmlns=""http://www.w3.org/2000/svg""")
        };
    }
}
