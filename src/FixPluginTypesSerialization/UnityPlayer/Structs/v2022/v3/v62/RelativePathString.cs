using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.Util;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v2022.v3.v62
{
    [ApplicableToUnityVersionsSince("2022.3.62")]
    public class RelativePathString : IRelativePathString
    {
        public RelativePathString()
        {

        }

        public RelativePathString(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer { get; set; }

        private unsafe char* data => *(char**)Pointer;
        private unsafe ulong length => *(ulong*)(Pointer + (nint)0x8);
        private unsafe ulong flags => *(ulong*)(Pointer + (nint)0x10);

        public unsafe string ToStringAnsi()
        {
            return Marshal.PtrToStringAnsi((nint)data, (int)length);
        }
    }
}
