using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v2019
{
    [StructLayout(LayoutKind.Explicit)]
    public struct MonoManagerStruct
    {
        [FieldOffset(0x1B0)] public AssemblyList m_AssemblyNames;
    }

    [ApplicableToUnityVersionsSince("2019.3.0")]
    public class MonoManager : IMonoManager
    {
        public MonoManager()
        {

        }

        public MonoManager(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer { get; set; }

        private unsafe MonoManagerStruct* _this => (MonoManagerStruct*)Pointer;

        public List<AssemblyStringStruct> ManagedAssemblyList = new();
        public int AssemblyCount => ManagedAssemblyList.Count;

        public unsafe void CopyNativeAssemblyListToManaged()
        {
            ManagedAssemblyList.Clear();

            ulong i = 0;
            for (AssemblyStringStruct* s = (AssemblyStringStruct*)_this->m_AssemblyNames.ptr;
                i < _this->m_AssemblyNames.size;
                s++, i++)
            {
                var newAssemblyString = new AssemblyStringStruct
                {
                    capacity = s->capacity,
                    extra = s->extra,
                    label = s->label,
                    size = s->size,
                    data = s->data
                };

                ManagedAssemblyList.Add(newAssemblyString);
            }
        }

        public void AddAssembliesToManagedList(List<string> pluginAssemblyPaths)
        {
            foreach (var pluginAssemblyPath in pluginAssemblyPaths)
            {
                var pluginAssemblyName = Path.GetFileName(pluginAssemblyPath);
                var length = (ulong)pluginAssemblyName.Length;

                var assemblyString = new AssemblyStringStruct
                {
                    label = AssemblyStringStruct.ValidStringLabel,
                    data = Marshal.StringToHGlobalAnsi(pluginAssemblyName),
                    capacity = length,
                    size = length
                };

                ManagedAssemblyList.Add(assemblyString);
            }
        }

        public unsafe void AllocNativeAssemblyListFromManaged()
        {
            var nativeArray = (AssemblyStringStruct*)Marshal.AllocHGlobal(Marshal.SizeOf<AssemblyStringStruct>() * ManagedAssemblyList.Count);

            var i = 0;
            for (AssemblyStringStruct* s = nativeArray; i < ManagedAssemblyList.Count; s++, i++)
            {
                s->label = ManagedAssemblyList[i].label;
                s->size = ManagedAssemblyList[i].size;
                s->capacity = ManagedAssemblyList[i].capacity;
                s->extra = ManagedAssemblyList[i].extra;
                s->data = ManagedAssemblyList[i].data;
            }

            _this->m_AssemblyNames.ptr = (nint)nativeArray;
            _this->m_AssemblyNames.size = (ulong)ManagedAssemblyList.Count;
            _this->m_AssemblyNames.capacity = _this->m_AssemblyNames.size;
        }

        public unsafe void PrintAssemblies()
        {
            ulong i = 0;
            for (AssemblyStringStruct* s = (AssemblyStringStruct*)_this->m_AssemblyNames.ptr;
                i < _this->m_AssemblyNames.size;
                s++, i++)
            {
                if (!s->IsValid())
                    continue;

                Log.Warning($"Ass: {Marshal.PtrToStringAnsi(s->data, (int)s->size)} | label : {s->label:X}");
            }
        }
    }
}
