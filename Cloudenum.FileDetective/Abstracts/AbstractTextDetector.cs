using System.Text;

namespace Cloudenum.FileDetective.Abstracts
{
    public abstract class AbstractTextDetector : IFileDetector
    {
        public abstract string Description { get; }
        
        public abstract string MimeType { get; }

        public virtual bool Matches(byte[] fileBytes)
        {
            var fileContent = Encoding.UTF8.GetString(fileBytes);
            
        }
    }
}
