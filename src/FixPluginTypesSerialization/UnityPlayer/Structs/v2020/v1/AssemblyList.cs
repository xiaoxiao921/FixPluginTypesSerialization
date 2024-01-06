using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v2020.v1
{
    // struct dynamic_array_detail::dynamic_array_data
    [StructLayout(LayoutKind.Sequential)]
    public struct AssemblyList
    {
        public nint ptr;
        public int label;
        public ulong size;
        public ulong capacity;
    }
}
