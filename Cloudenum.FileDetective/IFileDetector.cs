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
        /// <param name="fileBytes">Byte array of the file</param>
        /// <returns>
        /// True if the file bytes matches this detector, false otherwise
        /// </returns>
        bool Matches(byte[] fileBytes);
    }
}
