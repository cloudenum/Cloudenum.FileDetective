using System.Text.RegularExpressions;
using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    /// <summary>
    /// Detects JavaScript Object Notation files
    /// </summary>
    public class JsonDetector : AbstractRegexDetector
    {
        /// <inheritdoc/>
        public override string Description { get; } = "JavaScript Object Notation";

        /// <inheritdoc/>
        public override string MimeType { get; } = "application/json";

        /// <inheritdoc/>
        protected override Regex[] Regexes { get; } =
        {
            new Regex(@"\A\s*{", RegexOptions.Compiled),
            new Regex(@"\A\s*\[", RegexOptions.Compiled)
        };
    }
}
