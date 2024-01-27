using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.Util;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v2019.v1
{
    [StructLayout(LayoutKind.Explicit)]
    public struct MonoManagerStruct
    {
        [FieldOffset(0x1b0)] public DynamicArrayData m_AssemblyNames;
    }

    [ApplicableToUnityVersionsSince("2019.1.0")]
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

        private DynamicArrayData _originalAssemblyNames;

        public List<StringStorageDefaultV1> ManagedAssemblyList = new();
        public int AssemblyCount => ManagedAssemblyList.Count;

        public unsafe void CopyNativeAssemblyListToManaged()
        {
            MonoManagerCommon.CopyNativeAssemblyListToManagedV2(ManagedAssemblyList, _this->m_AssemblyNames);
        }

        public void AddAssembliesToManagedList(List<string> pluginAssemblyPaths)
        {
            MonoManagerCommon.AddAssembliesToManagedListV1(ManagedAssemblyList, pluginAssemblyPaths);
        }

        public unsafe void AllocNativeAssemblyListFromManaged()
        {
            _originalAssemblyNames = _this->m_AssemblyNames;
            MonoManagerCommon.AllocNativeAssemblyListFromManagedV2(ManagedAssemblyList, &_this->m_AssemblyNames);
        }

        public unsafe void PrintAssemblies()
        {
            MonoManagerCommon.PrintAssembliesV2(_this->m_AssemblyNames);
        }

        public unsafe void RestoreOriginalAssemblyNamesArrayPtr()
        {
            _this->m_AssemblyNames = _originalAssemblyNames;
        }
    }
}
