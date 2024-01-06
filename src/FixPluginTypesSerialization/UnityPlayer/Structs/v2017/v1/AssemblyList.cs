using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v2017.v1
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AssemblyList
    {
        public AssemblyStringStruct* first;
        public AssemblyStringStruct* last;
        public AssemblyStringStruct* end;
    }
}
