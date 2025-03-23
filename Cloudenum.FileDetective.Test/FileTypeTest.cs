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
        public void UnknownFile_Should_Return_Null()
        {
            var mimeType = GetMimeTypeFromTestSubject("Test.unknown");
            
            Assert.Null(mimeType);
        }
    }
}
