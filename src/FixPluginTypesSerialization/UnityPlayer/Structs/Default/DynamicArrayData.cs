using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.Default
{
    // struct dynamic_array_detail::dynamic_array_data
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct DynamicArrayData
    {
        public nint ptr;
        public int label;
        public ulong size;
        public ulong capacity;
    }
}
