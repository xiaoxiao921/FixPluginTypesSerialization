using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    public struct MonoManager
    {
        [FieldOffset(0x198)] public AssemblyList m_AssemblyNames;
    }
}
