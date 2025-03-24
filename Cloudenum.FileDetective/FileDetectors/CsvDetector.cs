using System.Collections.Generic;
using System.IO;
using System.Text;
using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    /// <summary>
    /// Detects Comma-Separated Values files
    /// </summary>
    public class CsvDetector : AbstractTextDetector
    {
        /// <inheritdoc/>
        public override string Description => "Comma-Separated Values";

        /// <inheritdoc/>
        public override string MimeType => "text/csv";

        /// <inheritdoc/>
        protected override string[] TextSignatures { get; } = { };

        /// <inheritdoc/>
        public override bool Matches(Stream stream)
        {
            if (!TextDetectorHelper.IsText(stream))
            {
                return false;
            }

            // Read the first few lines of the file
            List<string> lines = ReadLines(stream, maxLines: 3);
            if (lines.Count == 0)
                return false;

            // Attempt to detect the delimiter
            char? delimiter = DetectDelimiter(lines);
            if (delimiter == null)
                return false;

            // Check for consistent field counts
            int fieldCount = -1;
            foreach (string line in lines)
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Split the line using the detected delimiter
                string[] fields = SplitCsvLine(line, delimiter.Value);
                if (fieldCount == -1)
                {
                    fieldCount = fields.Length;
                }
                else if (fields.Length != fieldCount)
                {
                    return false;
                }
            }

            return true;
        }

        private List<string> ReadLines(Stream stream, int maxLines)
        {
            var lines = new List<string>();
            stream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true))
            {
                string line;
                while (lines.Count < maxLines && (line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }

        private char? DetectDelimiter(List<string> lines)
        {
            char[] possibleDelimiters = { ',', ';', '\t', '|' };
            foreach (char delimiter in possibleDelimiters)
            {
                int? fieldCount = null;
                bool isConsistent = true;
                foreach (string line in lines)
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] fields = SplitCsvLine(line, delimiter);
                    if (fieldCount == null)
                    {
                        fieldCount = fields.Length;
                    }
                    else if (fields.Length != fieldCount)
                    {
                        isConsistent = false;
                        break;
                    }
                }
                if (isConsistent && fieldCount > 1)
                    return delimiter;
            }
            return null;
        }

        private string[] SplitCsvLine(string line, char delimiter)
        {
            // Basic CSV parsing handling quoted fields
            var fields = new List<string>();
            var fieldBuilder = new StringBuilder();
            bool inQuotes = false;
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '"' && (i == 0 || line[i - 1] != '\\'))
                {
                    inQuotes = !inQuotes;
                    continue;
                }
                if (c == delimiter && !inQuotes)
                {
                    fields.Add(fieldBuilder.ToString());
                    fieldBuilder.Clear();
                }
                else
                {
                    fieldBuilder.Append(c);
                }
            }
            fields.Add(fieldBuilder.ToString());
            return fields.ToArray();
        }
    }
}
