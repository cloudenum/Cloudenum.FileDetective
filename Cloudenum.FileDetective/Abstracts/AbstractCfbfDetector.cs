using OpenMcdf;
using System;
using System.Collections;
using System.IO;

namespace Cloudenum.FileDetective.Abstracts
{
    /// <summary>
    /// Common abstract class for Compound File Binary Format (CFBF) detectors
    /// </summary>
    public abstract class AbstractCfbfDetector : IFileDetector
    {
        /// <summary>
        /// Represents the type of content to search for in the CFBF file
        /// </summary>
        protected enum CompoundFileContentType
        {
            /// <summary>
            /// Storage content type
            /// </summary>
            Storage,

            /// <summary>
            /// Stream content type
            /// </summary>
            Stream
        }

        /// <summary>
        /// Represents a content to search for in the CFBF file
        /// </summary>
        protected struct CompoundFileContent
        {
            /// <summary>
            /// Content type
            /// </summary>
            public CompoundFileContentType Type;

            /// <summary>
            /// Content name
            /// </summary>
            public string Name;
        }

        /// <inheritdoc/>
        public abstract string Description { get; }

        /// <inheritdoc/>
        public abstract string MimeType { get; }

        /// <summary>
        /// Represents the generic CFBF file signature
        /// </summary>
        public static readonly FileSignature CfbfSignature = new FileSignature()
        {
            Offset = 0,
            MagicBytes = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }
        };

        /// <summary>
        /// Array of contents to search for in the CFBF file
        /// </summary>
        protected abstract CompoundFileContent[] Contents { get; }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException"/>
        public virtual bool Matches(Stream stream)
        {
            using (CompoundFile cf = new CompoundFile(stream, CFSUpdateMode.ReadOnly, CFSConfiguration.LeaveOpen))
            {
                foreach (CompoundFileContent content in Contents)
                {
                    switch (content.Type)
                    {
                        case CompoundFileContentType.Storage:
                            if (!cf.RootStorage.ContainsStorage(content.Name))
                            {
                                return false;
                            }
                            break;
                        case CompoundFileContentType.Stream:
                            if (!cf.RootStorage.ContainsStream(content.Name))
                            {
                                return false;
                            }
                            break;
                        default:
                            throw new ArgumentException("Unknown content type", nameof(content.Type));
                    }
                }

                return true;
            }
        }
    }
}
