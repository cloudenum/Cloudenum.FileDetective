using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cloudenum.FileDetective
{
    /// <summary>
    /// Represents a sequence of file signatures
    /// </summary>
    public class FileSignatureSequence : IList<FileSignature>
    {
        private readonly List<FileSignature> _signatures = new List<FileSignature>();

        /// <inheritdoc/>
        public FileSignature this[int index] { get => _signatures[index]; set => _signatures[index] = value; }

        /// <inheritdoc/>
        public int Count => _signatures.Count;

        /// <summary>
        /// Get the length of the <see cref="FileSignatureSequence"/> in bytes
        /// </summary>
        public int Length => _signatures.Max(s => s.Offset) + _signatures.Sum(s => s.MagicBytes.Length);

        /// <inheritdoc/>
        public bool IsReadOnly => true;

        /// <inheritdoc/>
        public void Add(FileSignature item)
        {
            _signatures.Add(item);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _signatures.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(FileSignature item)
        {
            return _signatures.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(FileSignature[] array, int arrayIndex)
        {
            _signatures.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public IEnumerator<FileSignature> GetEnumerator()
        {
            return _signatures.GetEnumerator();
        }

        /// <inheritdoc/>
        public int IndexOf(FileSignature item)
        {
            return _signatures.IndexOf(item);
        }

        /// <inheritdoc/>
        public void Insert(int index, FileSignature item)
        {
            _signatures.Insert(index, item);
        }

        /// <inheritdoc/>
        public bool Remove(FileSignature item)
        {
            return _signatures.Remove(item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            _signatures.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _signatures.GetEnumerator();
        }
    }
}
