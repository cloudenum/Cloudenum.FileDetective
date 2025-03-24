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
        private static readonly Dictionary<int, Dictionary<short, List<IFileDetector>>> SignatureDetectors = new Dictionary<int, Dictionary<short, List<IFileDetector>>>();

        static FileType()
        {
            // Signature Detectors
            RegisterFileDetector<AviDetector>();
            RegisterFileDetector<Jpeg2000Detector>();
            RegisterFileDetector<JpegDetector>();
            RegisterFileDetector<PdfDetector>();
            RegisterFileDetector<PngDetector>();
            RegisterFileDetector<GifDetector>();
            RegisterFileDetector<BmpDetector>();
            RegisterFileDetector<TiffDetector>();
            RegisterFileDetector<WebpDetector>();

            // ZIP Based Detectors
            RegisterFileDetector<ZipArchiveDetector>();
            RegisterFileDetector<DocxDetector>();
            RegisterFileDetector<XlsxDetector>();
            RegisterFileDetector<PptxDetector>();

            // CFBF Based Detectors
            RegisterFileDetector<DocDetector>();
            RegisterFileDetector<XlsDetector>();
            RegisterFileDetector<PptDetector>();

            // Text Based Detectors
            RegisterFileDetector<SvgDetector>();
            RegisterFileDetector<JsonDetector>();
            RegisterFileDetector<CsvDetector>();
            RegisterFileDetector<PlainTextDetector>();
        }

        private static short GetByteArrayPrefix(byte[] bytes, int offset = 0)
        {
            byte[] twoBytes;
            if (bytes.Length > 2)
            {
                twoBytes = bytes.Skip(offset).Take(2).ToArray();
            }
            else
            {
                twoBytes = bytes;
            }

            return BitConverter.ToInt16(twoBytes, 0);
        }

        private static void RegisterSignatureDetector(IFileDetector signatureDetector, FileSignature signature, bool prepend = false)
        {
            if (signature.MagicBytes.Length < 2)
            {
                GeneralDetectors.Add(signatureDetector);
                return;
            }

            var prefix = GetByteArrayPrefix(signature.MagicBytes);
            int offset = signature.Offset;

            if (!SignatureDetectors.ContainsKey(offset))
            {
                SignatureDetectors[offset] = new Dictionary<short, List<IFileDetector>>();
            }

            if (!SignatureDetectors[offset].ContainsKey(prefix))
            {
                SignatureDetectors[offset][prefix] = new List<IFileDetector>();
            }

            if (prepend)
            {
                SignatureDetectors[offset][prefix].Insert(0, signatureDetector);
            }
            else
            {
                SignatureDetectors[offset][prefix].Add(signatureDetector);
            }
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
                    RegisterSignatureDetector(fileDetector, signature);
                }
            }
            else if (fileDetector is AbstractSignatureSequenceDetector signatureSequenceDetector)
            {
                foreach (var sequence in signatureSequenceDetector.SignatureSequences)
                {
                    if (sequence.Count > 0)
                    {
                        RegisterSignatureDetector(fileDetector, sequence[0]);
                    }
                }
            }
            else if (fileDetector is AbstractZipBasedDetector)
            {
                RegisterSignatureDetector(
                    fileDetector,
                    signature: AbstractZipBasedDetector.ZipSignature,
                    prepend: true);
            }
            else if (fileDetector is AbstractCfbfDetector)
            {
                RegisterSignatureDetector(
                    fileDetector,
                    signature: AbstractCfbfDetector.CfbfSignature,
                    prepend: true);
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
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
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
        /// The MIME type or null if the file is not recognized
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
                int bufferSize = 2;
                byte[] buffer = new byte[bufferSize];
                stream.Position = offset;
                int bytesRead = stream.Read(buffer, 0, bufferSize);

                if (bytesRead < bufferSize)
                {
                    Array.Resize(ref buffer, bytesRead);
                }

                short prefix = GetByteArrayPrefix(buffer, 0);
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
