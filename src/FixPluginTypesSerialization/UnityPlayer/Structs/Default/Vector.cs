using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.Default
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Vector<T> where T : struct
    {
#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
        public T* first;
        public T* last;
        public T* end;
#pragma warning restore CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
    }
}
