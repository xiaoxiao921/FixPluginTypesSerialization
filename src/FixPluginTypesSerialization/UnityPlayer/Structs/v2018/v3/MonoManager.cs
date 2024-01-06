using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v2018.v3
{
    [StructLayout(LayoutKind.Explicit)]
    public struct MonoManagerStruct
    {
        [FieldOffset(0x198)] public AssemblyList m_AssemblyNames;
    }

    [ApplicableToUnityVersionsSince("2018.1.0")]
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

        private AssemblyList _originalAssemblyNames;

        public List<AssemblyStringStruct> ManagedAssemblyList = new();
        public int AssemblyCount => ManagedAssemblyList.Count;

        public unsafe void CopyNativeAssemblyListToManaged()
        {
            ManagedAssemblyList.Clear();

            for (AssemblyStringStruct* s = _this->m_AssemblyNames.first; s != _this->m_AssemblyNames.last; s++)
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
            var nativeArray = (AssemblyStringStruct*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(AssemblyStringStruct)) * ManagedAssemblyList.Count);

            var i = 0;
            for (AssemblyStringStruct* s = nativeArray; i < ManagedAssemblyList.Count; s++, i++)
            {
                s->label = ManagedAssemblyList[i].label;
                s->size = ManagedAssemblyList[i].size;
                s->capacity = ManagedAssemblyList[i].capacity;
                s->extra = ManagedAssemblyList[i].extra;
                s->data = ManagedAssemblyList[i].data;
            }

            _originalAssemblyNames = _this->m_AssemblyNames;

            _this->m_AssemblyNames.first = nativeArray;
            _this->m_AssemblyNames.last = nativeArray + ManagedAssemblyList.Count;
            _this->m_AssemblyNames.end = _this->m_AssemblyNames.last;
        }

        public unsafe void PrintAssemblies()
        {
            for (AssemblyStringStruct* s = _this->m_AssemblyNames.first; s != _this->m_AssemblyNames.last; s++)
            {
                if (!s->IsValid())
                    continue;

                Log.Warning($"Ass: {Marshal.PtrToStringAnsi(s->data, (int)s->size)}");
            }
        }

        public unsafe void RestoreOriginalAssemblyNamesArrayPtr()
        {
            _this->m_AssemblyNames = _originalAssemblyNames;
        }
    }
}
