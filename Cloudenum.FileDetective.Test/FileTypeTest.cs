namespace Cloudenum.FileDetective.Test
{
    public class FileTypeTest
    {
        private static byte[] ReadAllBytesFromTestSubject(string fileName)
        {
            var filePath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "TestSubjects", fileName);
            return System.IO.File.ReadAllBytes(filePath);
        }

        [Fact]
        public void AviDetector_Should_Detect_Avi_File()
        {
            byte[] aviFileBytes = ReadAllBytesFromTestSubject("Test.avi");

            // Act
            var mimeType = FileType.GetMimeType(aviFileBytes);

            // Assert
            Assert.Equal("video/avi", mimeType);
        }

        [Fact]
        public void JpegDetector_Should_Detect_Jpeg_File()
        {
            byte[] jpegFileBytes = ReadAllBytesFromTestSubject("Test.jpg");

            // Act
            var mimeType = FileType.GetMimeType(jpegFileBytes);

            // Assert
            Assert.Equal("image/jpeg", mimeType);
        }

        [Fact]
        public void Jpeg2000Detector_Should_Detect_Jpeg2000_File()
        {
            byte[] jpeg2000FileBytes = ReadAllBytesFromTestSubject("Test.jp2");

            // Act
            var mimeType = FileType.GetMimeType(jpeg2000FileBytes);

            // Assert
            Assert.Equal("image/jp2", mimeType);
        }

        [Fact]
        public void PdfDetector_Should_Detect_Pdf_File()
        {
            byte[] pdfFileBytes = ReadAllBytesFromTestSubject("Test.pdf");

            // Act
            var mimeType = FileType.GetMimeType(pdfFileBytes);

            // Assert
            Assert.Equal("application/pdf", mimeType);
        }

        [Fact]
        public void PngDetector_Should_Detect_Png_File()
        {
            byte[] pngFileBytes = ReadAllBytesFromTestSubject("Test.png");

            // Act
            var mimeType = FileType.GetMimeType(pngFileBytes);

            // Assert
            Assert.Equal("image/png", mimeType);
        }

        [Fact]
        public void ZipArchiveDetector_Should_Detect_Zip_File()
        {
            byte[] zipFileBytes = ReadAllBytesFromTestSubject("Test.zip");

            // Act
            var mimeType = FileType.GetMimeType(zipFileBytes);

            // Assert
            Assert.Equal("application/zip", mimeType);
        }

        [Fact]
        public void DocxDetector_Should_Detect_Docx_File()
        {
            byte[] docxFileBytes = ReadAllBytesFromTestSubject("Test.docx");

            // Act
            var mimeType = FileType.GetMimeType(docxFileBytes);

            // Assert
            Assert.Equal("application/vnd.openxmlformats-officedocument.wordprocessingml.document", mimeType);
        }

        [Fact]
        public void XlsxDetector_Should_Detect_Xlsx_File()
        {
            byte[] xlsxFileBytes = ReadAllBytesFromTestSubject("Test.xlsx");

            // Act
            var mimeType = FileType.GetMimeType(xlsxFileBytes);

            // Assert
            Assert.Equal("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", mimeType);
        }
    }
}
