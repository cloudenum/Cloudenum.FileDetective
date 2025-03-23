# Cloudenum.FileDetective
A C# library to detect file types by analyzing their content.

## Usage
```csharp
using Cloudenum.FileDetective;

using (var file = File.OpenRead("path/to/file"))
{
    string mimeType = FileType.GetMimeType(file);
    Console.WriteLine(mimeType);

    string[] extensions = FileType.GetExtensions(file);
    foreach (string extension in extensions)
    {
        Console.WriteLine(extension);
    }
})
```

### Write Your Own File Detector
You can create a custom file detector by implementing the `IFileDetector` interface.
Then register it using the `FileType.RegisterFileDetector` method before using this library.  
You can register it during program startup (e.g. in `Program.cs` or `Startup.cs`).
```csharp
FileType.RegisterFileDetector<CustomFileDetector>();
```

For easier implementation, you can inherit from these abstract classes:
- `AbstractRegexDetector` for regex-based detection.
- `AbstractSignatureDetector` for detection based on file signatures.
- `AbstractSignatureSequenceDetector` for detection based on file signature sequences.
- `AbstractTextDetector` for text-based detection.
- `AbstractZipBasedDetector` for detection based on ZIP file structure.

```csharp
public class CustomFileDetector : AbstractSignatureDetector
{
    public override string Description { get; } = "Custom File";
    public override string MimeType { get; } = "application/custom";
    public override FileSignature[] Signatures { get; } = {
        new FileSignature() {
            Offset = 0,
            MagicBytes = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF },
        },
    }
}
```

## Limitations
- Currently, detection for text files only support Windows and Linux.
- There may be some false positives if the file has been modified in such a way to appear as another file type.

## License
This project is licensed under the Mozilla Public License 2.0 - see the [LICENSE](LICENSE.txt) file for details.
