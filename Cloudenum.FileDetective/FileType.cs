using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cloudenum.FileDetective.Abstracts;
using Cloudenum.FileDetective.FileDetectors;

namespace Cloudenum.FileDetective
{
    /// <summary>
    /// Analyzes the type of a file based on its content
    /// </summary>
    public class FileType
    {
        private static readonly List<IFileDetector> GeneralDetectors = new List<IFileDetector>();
        private static readonly Dictionary<int, Dictionary<int, List<IFileDetector>>> SignatureDetectors = new Dictionary<int, Dictionary<int, List<IFileDetector>>>();

        static FileType()
        {
            RegisterFileDetector<AviDetector>();
            RegisterFileDetector<Jpeg2000Detector>();
            RegisterFileDetector<JpegDetector>();
            RegisterFileDetector<PdfDetector>();
            RegisterFileDetector<PngDetector>();

            // ZIP Based Detectors
            RegisterFileDetector<ZipArchiveDetector>();
            RegisterFileDetector<DocxDetector>();
            RegisterFileDetector<XlsxDetector>();
        }

        private static int GetByteArrayPrefix(byte[] bytes, int offset = 0)
        {
            var fourBytes = bytes.Skip(offset).Take(4);
            var count = fourBytes.Count();
            if (count < 4)
            {
                fourBytes = fourBytes.Concat(Enumerable.Repeat((byte)0, 4 - count));
            }

            return BitConverter.ToInt32(fourBytes.ToArray(), 0);
        }

        private static void RegisterZipDetector(IFileDetector fileDetector)
        {
            var offset = AbstractZipBasedDetector.ZipSignature.Offset;
            var zipSignaturePrefix = GetByteArrayPrefix(AbstractZipBasedDetector.ZipSignature.MagicBytes);

            if (!SignatureDetectors.ContainsKey(offset))
            {
                SignatureDetectors[offset] = new Dictionary<int, List<IFileDetector>>();
            }

            if (!SignatureDetectors[offset].ContainsKey(zipSignaturePrefix))
            {
                SignatureDetectors[offset][zipSignaturePrefix] = new List<IFileDetector>();
            }

            SignatureDetectors[offset][zipSignaturePrefix].Insert(0, fileDetector);
        }

        private static void RegisterSignatureDetector(IFileDetector signatureDetector, int offset, byte[] magicBytes)
        {
            var prefix = GetByteArrayPrefix(magicBytes);

            if (!SignatureDetectors.ContainsKey(offset))
            {
                SignatureDetectors[offset] = new Dictionary<int, List<IFileDetector>>();
            }

            if (!SignatureDetectors[offset].ContainsKey(prefix))
            {
                SignatureDetectors[offset][prefix] = new List<IFileDetector>();
            }

            SignatureDetectors[offset][prefix].Add(signatureDetector);
        }

        /// <summary>
        /// Register a file detector
        /// </summary>
        /// <remarks>
        /// The file signature must implements the <see cref="IFileDetector"/> interface
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        public static void RegisterFileDetector<T>() where T : IFileDetector, new()
        {
            RegisterFileDetector(new T());
        }

        /// <summary>
        /// Register a file detector with a custom implementation
        /// </summary>
        /// <param name="fileDetector">Custom implementation of file detector</param>
        public static void RegisterFileDetector(IFileDetector fileDetector)
        {
            if (fileDetector is AbstractSignatureDetector signatureDetector)
            {
                foreach (var signature in signatureDetector.Signatures)
                {
                    RegisterSignatureDetector(fileDetector, signature.Offset, signature.MagicBytes);
                }
            }
            else if (fileDetector is AbstractSignatureSequenceDetector signatureSequenceDetector)
            {
                foreach (var sequence in signatureSequenceDetector.SignatureSequences)
                {
                    if (sequence.Count > 0)
                    {
                        RegisterSignatureDetector(fileDetector, sequence[0].Offset, sequence[0].MagicBytes);
                    }
                }
            }
            else if (fileDetector is AbstractZipBasedDetector)
            {
                RegisterZipDetector(fileDetector);
            }
            else
            {
                GeneralDetectors.Add(fileDetector);
            }
        }

        /// <summary>
        /// Get every file extensions that corresponds to the file MIME type
        /// </summary>
        /// <param name="stream">Source stream of the file</param>
        /// <returns>
        /// The file extensions without leading dot or null if the file is not recognized
        /// </returns>
        public static string[] GetFileExtensions(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return GetFileExtensions(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// Get every file extensions that corresponds to the file MIME type
        /// </summary>
        /// <param name="fileBytes">Byte array of the file</param>
        /// <returns>
        /// The file extensions without leading dot or null if the file is not recognized
        /// </returns>
        public static string[] GetFileExtensions(byte[] fileBytes)
        {
            var mime = GetMimeType(fileBytes);

            return mime != null ? MimeTypes.GetMimeTypeExtensions(mime).ToArray() : null;
        }

        /// <summary>
        /// Get the MIME type of a file
        /// </summary>
        /// <param name="stream">Source stream of the file</param>
        /// <returns>
        /// The file extensions without leading dot or null if the file is not recognized
        /// </returns>
        public static string GetMimeType(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return GetMimeType(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// Get the MIME type of a file
        /// </summary>
        /// <param name="fileBytes">Byte array of the file</param>
        /// <returns>
        /// MIME type of the file or null if the file is not recognized
        /// </returns>
        public static string GetMimeType(byte[] fileBytes)
        {
            if (fileBytes == null || fileBytes.Length == 0)
            {
                return null;
            }

            // First check the signature detectors
            foreach (var offsetGroup in SignatureDetectors)
            {
                var offset = offsetGroup.Key;
                var prefix = GetByteArrayPrefix(fileBytes, offset);
                if (offsetGroup.Value.ContainsKey(prefix))
                {
                    foreach (var detector in offsetGroup.Value[prefix])
                    {
                        if (detector.Matches(fileBytes))
                        {
                            return detector.MimeType;
                        }
                    }
                }
            }

            // Fallback to general detectors
            foreach (var detector in GeneralDetectors)
            {
                if (detector.Matches(fileBytes))
                {
                    return detector.MimeType;
                }
            }
            return null;
        }
    }
}
