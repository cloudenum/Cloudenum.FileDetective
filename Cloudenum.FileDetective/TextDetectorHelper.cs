using System;
using System.IO;

namespace Cloudenum.FileDetective
{
    internal static class TextDetectorHelper
    {
        /// <summary>
        /// Detect encoding of the file
        /// </summary>
        /// <param name="fileBytes"></param>
        /// <returns>
        /// Encoding name or null if encoding is not detected
        /// </returns>
        public static string DetectEncoding(byte[] fileBytes)
        {
            string encodingName;
            using (var detector = new CharsetDetector())
            {
                var result = detector.Detect(fileBytes);
                if (result.HasValue)
                {
                    encodingName = result.Value.Encoding;
                }
                else
                {
                    encodingName = null;
                }
            }

            return encodingName;
        }

        public static bool IsText(Stream stream)
        {
            int bufferSize = 4096;
            byte[] buffer = new byte[bufferSize];
            stream.Position = 0;
            int bytesRead = stream.Read(buffer, 0, bufferSize);

            if (bytesRead == 0)
            {
                return false;
            }

            if (bytesRead < bufferSize)
            {
                Array.Resize(ref buffer, bytesRead);
            }

            return TextDetectorHelper.DetectEncoding(buffer) != null;
        }
    }
}
