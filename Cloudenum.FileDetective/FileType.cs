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

            // Text Based Detectors
            RegisterFileDetector<CsvDetector>();
            RegisterFileDetector<PlainTextDetector>();
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
            var mime = GetMimeType(stream);

            return mime != null ? MimeTypes.GetMimeTypeExtensions(mime).ToArray() : null;
        }

        /// <summary>
        /// Get the MIME type of a file
        /// </summary>
        /// <param name="stream">Source stream of the file</param>
        /// <returns>
        /// The file extensions without leading dot or null if the file is not recognized
        /// </returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public static string GetMimeType(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanSeek || !stream.CanRead)
            {
                throw new ArgumentException("Stream must be seekable and readable", nameof(stream));
            }

            if (stream.Length == 0)
            {
                return null;
            }

            // First check the signature detectors
            foreach (var offsetGroup in SignatureDetectors)
            {
                int offset = offsetGroup.Key;
                int bufferSize = 4;
                byte[] buffer = new byte[bufferSize];
                stream.Position = 0;
                int bytesRead = stream.Read(buffer, offset, buffer.Length);

                if (bytesRead < bufferSize)
                {
                    Array.Resize(ref buffer, bytesRead);
                }

                int prefix = GetByteArrayPrefix(buffer, offset);
                if (offsetGroup.Value.ContainsKey(prefix))
                {
                    foreach (IFileDetector detector in offsetGroup.Value[prefix])
                    {
                        stream.Position = 0;
                        if (detector.Matches(stream))
                        {
                            return detector.MimeType;
                        }
                    }
                }
            }

            // Fallback to general detectors
            foreach (IFileDetector detector in GeneralDetectors)
            {
                stream.Position = 0;
                if (detector.Matches(stream))
                {
                    return detector.MimeType;
                }
            }

            return null;
        }
    }
}
