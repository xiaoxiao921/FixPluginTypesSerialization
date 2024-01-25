using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.Util;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v5.v2
{
    [StructLayout(LayoutKind.Explicit)]
    public struct MonoManagerStruct
    {
        [FieldOffset(0x678)] public Vector<StringStorageDefaultV0> m_AssemblyNames;
    }

    [ApplicableToUnityVersionsSince("5.2.0")]
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

        private Vector<StringStorageDefaultV0> _originalAssemblyNames;

        public List<StringStorageDefaultV0> ManagedAssemblyList = new();
        public int AssemblyCount => ManagedAssemblyList.Count;

        public unsafe void CopyNativeAssemblyListToManaged()
        {
            MonoManagerCommon.CopyNativeAssemblyListToManagedV0(ManagedAssemblyList, _this->m_AssemblyNames);
        }

        public void AddAssembliesToManagedList(List<string> pluginAssemblyPaths)
        {
            MonoManagerCommon.AddAssembliesToManagedListV0(ManagedAssemblyList, pluginAssemblyPaths);
        }

        public unsafe void AllocNativeAssemblyListFromManaged()
        {
            _originalAssemblyNames = _this->m_AssemblyNames;
            MonoManagerCommon.AllocNativeAssemblyListFromManagedV0(ManagedAssemblyList, &_this->m_AssemblyNames);
        }

        public unsafe void PrintAssemblies()
        {
            MonoManagerCommon.PrintAssembliesV0(_this->m_AssemblyNames);
        }

        public unsafe void RestoreOriginalAssemblyNamesArrayPtr()
        {
            _this->m_AssemblyNames = _originalAssemblyNames;
        }
    }
}
