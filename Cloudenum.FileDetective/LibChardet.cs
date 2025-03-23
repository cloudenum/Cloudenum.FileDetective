using System;
using System.Runtime.InteropServices;

namespace Cloudenum.FileDetective
{
    internal class LibChardet
    {
        private const string WindowsLibName = "libchardet.dll";
        private const string LinuxLibName = "libchardet.so";

        // Struct matching the DetectObj structure from the native library
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

        #region Windows
        // Function to initialize a DetectObj instance
        [DllImport(WindowsLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "detect_obj_init")]
        private static extern IntPtr windows_detect_obj_init();

        // Function to free a DetectObj instance
        [DllImport(WindowsLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "detect_obj_free")]
        private static extern void windows_detect_obj_free(ref IntPtr detectObj);

        [DllImport(WindowsLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "detect_r")]
        private static extern short windows_detect_r(byte[] data, UIntPtr length, ref IntPtr detectObj);
        #endregion

        #region Linux
        [DllImport(LinuxLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "detect_obj_init")]
        private static extern IntPtr linux_detect_obj_init();

        [DllImport(LinuxLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "detect_obj_free")]
        private static extern void linux_detect_obj_free(ref IntPtr detectObj);

        [DllImport(LinuxLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "detect_r")]
        private static extern short linux_detect_r(byte[] data, UIntPtr length, ref IntPtr detectObj);
        #endregion

        public static IntPtr DetectObjInit()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return windows_detect_obj_init();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return linux_detect_obj_init();
            }

            throw new PlatformNotSupportedException("Unsupported platform");
        }

        public static void DetectObjFree(ref IntPtr detectObj)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                windows_detect_obj_free(ref detectObj);
                return;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                linux_detect_obj_free(ref detectObj);
                return;
            }

            throw new PlatformNotSupportedException("Unsupported platform");
        }

        public static short DetectR(byte[] data, UIntPtr length, ref IntPtr detectObj)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return windows_detect_r(data, length, ref detectObj);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return linux_detect_r(data, length, ref detectObj);
            }

            throw new PlatformNotSupportedException("Unsupported platform");
        }
    }
}
