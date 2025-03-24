namespace Cloudenum.FileDetective.Test
{
    public class FileTypeTest
    {
        private static string GetTestSubjectPath(string fileName)
        {
            return System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "TestSubjects", fileName);
        }

        private static string GetMimeTypeFromTestSubject(string fileName)
        {
            string filePath = GetTestSubjectPath(fileName);
            using var fileStream = System.IO.File.OpenRead(filePath);
            return FileType.GetMimeType(fileStream);
        }

        [Fact]
        public void AviDetector_Should_Detect_Avi_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.avi");

            Assert.Equal("video/avi", mimeType);
        }

        [Fact]
        public void JpegDetector_Should_Detect_Jpeg_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.jpg");

            Assert.Equal("image/jpeg", mimeType);
        }

        [Fact]
        public void Jpeg2000Detector_Should_Detect_Jpeg2000_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.jp2");

            Assert.Equal("image/jp2", mimeType);
        }

        [Fact]
        public void GifDetector_Should_Detect_Gif_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.gif");
            Assert.Equal("image/gif", mimeType);
        }

        [Fact]
        public void BmpDetector_Should_Detect_Bmp_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.bmp");
            Assert.Equal("image/bmp", mimeType);
        }

        [Fact]
        public void TiffDetector_Should_Detect_Tiff_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.tiff");
            Assert.Equal("image/tiff", mimeType);
        }

        [Fact]
        public void WebpDetector_Should_Detect_Webp_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.webp");
            Assert.Equal("image/webp", mimeType);
        }

        [Fact]
        public void PdfDetector_Should_Detect_Pdf_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.pdf");

            Assert.Equal("application/pdf", mimeType);
        }

        [Fact]
        public void PngDetector_Should_Detect_Png_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.png");

            Assert.Equal("image/png", mimeType);
        }

        [Fact]
        public void ZipArchiveDetector_Should_Detect_Zip_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.zip");

            Assert.Equal("application/zip", mimeType);
        }

        [Fact]
        public void DocxDetector_Should_Detect_Docx_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.docx");

            Assert.Equal("application/vnd.openxmlformats-officedocument.wordprocessingml.document", mimeType);
        }

        [Fact]
        public void XlsxDetector_Should_Detect_Xlsx_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.xlsx");

            Assert.Equal("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", mimeType);
        }

        [Fact]
        public void PptxDetector_Should_Detect_Pptx_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.pptx");
            Assert.Equal("application/vnd.openxmlformats-officedocument.presentationml.presentation", mimeType);
        }

        [Fact]
        public void DocDetector_Should_Detect_Doc_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.doc");
            Assert.Equal("application/msword", mimeType);
        }

        [Fact]
        public void XlsDetector_Should_Detect_Xls_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.xls");
            Assert.Equal("application/vnd.ms-excel", mimeType);
        }

        [Fact]
        public void PptDetector_Should_Detect_Ppt_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.ppt");
            Assert.Equal("application/vnd.ms-powerpoint", mimeType);
        }

        [Fact]
        public void PlainTextDetector_Should_Detect_Plain_Text_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.txt");
            
            Assert.Equal("text/plain", mimeType);
        }

        [Fact]
        public void CsvDetector_Should_Detect_Csv_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.csv");

            Assert.Equal("text/csv", mimeType);
        }

        [Fact]
        public void JsonDetector_Should_Detect_Json_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.json");

            Assert.Equal("application/json", mimeType);
        }

        [Fact]
        public void SvgDetector_Should_Detect_Svg_File()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.svg");
            Assert.Equal("image/svg+xml", mimeType);
        }

        [Fact]
        public void UnknownFile_Should_Return_Null()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.unknown");
            
            Assert.Null(mimeType);
        }
    }
}
