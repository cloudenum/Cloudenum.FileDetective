using System.Runtime.InteropServices;

namespace Cloudenum.FileDetective
{
    /// <summary>
    /// Represents a file signature
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FileSignature
    {
        /// <summary>
        /// Offset of the file signature
        /// </summary>
        public int Offset;

        /// <summary>
        /// Magic bytes of the file signature
        /// </summary>
        public byte[] MagicBytes;
    }
}
