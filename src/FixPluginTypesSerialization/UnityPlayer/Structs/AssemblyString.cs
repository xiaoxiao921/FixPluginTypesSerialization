using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs
{
    // core::StringStorageDefault<char> from ScriptingManager.h
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct AssemblyString
    {
        // MemLabelIdentifier::kMemStringId
        public const int ValidStringLabel = 0x45;

        public nint data;
        public ulong capacity;
        public ulong extra;
        public ulong size;
        public int label;

        public bool IsValid() => data != 0 && label == ValidStringLabel;
    }
}
