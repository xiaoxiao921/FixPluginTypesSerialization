using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AssemblyList
    {
        public AssemblyString* first;
        public AssemblyString* last;
        public AssemblyString* end;
    }
}
