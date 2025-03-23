using System.IO;

namespace Cloudenum.FileDetective
{
    /// <summary>
    /// Common interface for file detector
    /// </summary>
    public interface IFileDetector
    {
        /// <summary>
        /// File type description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// File MIME type
        /// </summary>
        string MimeType { get; }

        /// <summary>
        /// Determine if the file bytes matches this detector
        /// </summary>
        /// <param name="stream">Stream containing the file bytes</param>
        /// <returns>
        /// True if the file bytes matches this detector, false otherwise
        /// </returns>
        bool Matches(Stream stream);
    }
}
