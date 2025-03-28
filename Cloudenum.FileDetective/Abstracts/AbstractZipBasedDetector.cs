﻿using System.IO;
using System.IO.Compression;

namespace Cloudenum.FileDetective.Abstracts
{
    /// <summary>
    /// Common abstract class for detectors that relies on zip file contents
    /// </summary>
    public abstract class AbstractZipBasedDetector : IFileDetector
    {
        /// <inheritdoc/>
        public abstract string Description { get; }

        /// <inheritdoc/>
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
        /// Files to look for in the zip archive
        /// </summary>
        protected abstract string[] ZipEntries { get; }

        /// <inheritdoc/>
        public virtual bool Matches(Stream stream)
        {
            using (ZipArchive zipArchive = new ZipArchive(stream, mode: ZipArchiveMode.Read, leaveOpen: true))
            {
                foreach (var entryName in ZipEntries)
                {
                    if (zipArchive.GetEntry(entryName) == null)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
