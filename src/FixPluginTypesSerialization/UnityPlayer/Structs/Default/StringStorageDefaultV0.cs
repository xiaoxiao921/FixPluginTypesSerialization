using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.Default
{
    [StructLayout(LayoutKind.Sequential)]
    public struct StringStorageDefaultV0
    {
        public nint data;
        public ulong extra1;
        public ulong size;
        public ulong flags;
        public ulong extra2;
    }
}
