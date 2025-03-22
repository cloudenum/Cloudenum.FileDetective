using System.IO;
using System.IO.Compression;

namespace Cloudenum.FileDetective.Abstracts
{
    /// <summary>
    /// Common abstract class for detectors that relies on zip file contents
    /// </summary>
    public abstract class AbstractZipBasedDetector : IFileDetector
    {
        public abstract string Description { get; }

        public abstract string MimeType { get; }

        /// <summary>
        /// Zip file signature
        /// </summary>
        public readonly static FileSignature ZipSignature = new FileSignature()
        {
            Offset = 0,
            MagicBytes = new byte[] { 0x50, 0x4B, 0x03, 0x04 }
        };

        /// <summary>
        /// Contents to look for in the zip file
        /// </summary>
        protected abstract string[] ContentsToLookFor { get; }

        public virtual bool Matches(byte[] fileBytes)
        {
            if (fileBytes == null || fileBytes.Length == 0)
            {
                return false;
            }

            if (ContentsToLookFor == null || ContentsToLookFor.Length == 0)
            {
                return false;
            }

            using (var stream = new MemoryStream(fileBytes))
            using (ZipArchive zipArchive = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                foreach (var content in ContentsToLookFor)
                {
                    if (zipArchive.GetEntry(content) == null)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
