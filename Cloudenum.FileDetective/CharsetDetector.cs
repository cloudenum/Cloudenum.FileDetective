using System;
using System.Runtime.InteropServices;

namespace Cloudenum.FileDetective
{
    internal class CharsetDetector : IDisposable
    {
        private IntPtr _detectObj;

        internal struct DetectedCharset
        {
            public string Encoding;
            public float Confidence;
            public bool HasBom;
        }

        public CharsetDetector()
        {
            _detectObj = LibChardet.DetectObjInit();
            if (_detectObj == IntPtr.Zero)
            {
                throw new Exception("Failed to initialize DetectObj object");
            }
        }

        public DetectedCharset? Detect(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentException("Data cannot be null or empty", nameof(data));
            }

            UIntPtr length = new UIntPtr((uint)data.Length);

            switch (LibChardet.DetectR(data, length, ref _detectObj))
            {
                case LibChardet.CHARDET_SUCCESS:
                    {
                        LibChardet.DetectObj detectObj = Marshal.PtrToStructure<LibChardet.DetectObj>(_detectObj);
                        var result = new DetectedCharset
                        {
                            Encoding = Marshal.PtrToStringAnsi(detectObj.encoding),
                            Confidence = detectObj.confidence,
                            HasBom = detectObj.bom == 1
                        };

                        return result;
                    }
                case LibChardet.CHARDET_OUT_OF_MEMORY:
                    {
                        throw new OutOfMemoryException("Out of memory while calling libchardet detect_r()");
                    }
                case LibChardet.CHARDET_NULL_OBJECT:
                    {
                        throw new InvalidOperationException("DetectObj object is null");
                    }
            }

            return null;
        }

        public void Dispose()
        {
            if (_detectObj != IntPtr.Zero)
            {
                LibChardet.DetectObjFree(ref _detectObj);
                _detectObj = IntPtr.Zero;
            }
        }
    }
}
