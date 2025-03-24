using System;
using System.Runtime.InteropServices;

namespace Cloudenum.FileDetective
{
    internal class LibChardet
    {
        private const string LibName = "libchardet";

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DetectObj
        {
            public IntPtr encoding;
            public float confidence;
            public short bom;
        }

        public const short CHARDET_OUT_OF_MEMORY = -128;
        public const short CHARDET_MEM_ALLOCATED_FAIL = -127;
        public const short CHARDET_SUCCESS = 0;
        public const short CHARDET_NO_RESULT = 1;
        public const short CHARDET_NULL_OBJECT = 2;

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "detect_obj_init")]
        private static extern IntPtr detect_obj_init();

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "detect_obj_free")]
        private static extern void detect_obj_free(ref IntPtr detectObj);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "detect_r")]
        private static extern short detect_r(byte[] data, UIntPtr length, ref IntPtr detectObj);

        public static IntPtr DetectObjInit()
        {
            return detect_obj_init();
        }

        public static void DetectObjFree(ref IntPtr detectObj)
        {
            detect_obj_free(ref detectObj);
        }

        public static short DetectR(byte[] data, UIntPtr length, ref IntPtr detectObj)
        {
            return detect_r(data, length, ref detectObj);
        }
    }
}
