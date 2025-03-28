﻿using Cloudenum.FileDetective.Abstracts;

namespace Cloudenum.FileDetective.FileDetectors
{
    /// <summary>
    /// Detects AVI video files
    /// </summary>
    public class AviDetector : AbstractSignatureSequenceDetector
    {
        /// <inheritdoc/>
        public override string Description { get; } = "Audio Video Interleave";

        /// <inheritdoc/>
        public override string MimeType { get; } = "video/avi";

        /// <inheritdoc/>
        public override FileSignatureSequence[] SignatureSequences { get; } = new[] {
            new FileSignatureSequence()
            {
                new FileSignature()
                {
                    Offset = 0,
                    MagicBytes = new byte[] { 0x52, 0x49, 0x46, 0x46 }
                },
                new FileSignature()
                {
                    Offset = 8,
                    MagicBytes = new byte[] { 0x41, 0x56, 0x49, 0x20 }
                }
            }
        };
    }
}
