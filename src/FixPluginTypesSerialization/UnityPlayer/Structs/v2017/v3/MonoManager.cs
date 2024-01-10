﻿using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.Util;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v2017.v3
{
    [StructLayout(LayoutKind.Explicit)]
    public struct MonoManagerStruct
    {
        [FieldOffset(0x198)] public AssemblyList<StringStorageDefaultV1> m_AssemblyNames;
    }

    [ApplicableToUnityVersionsSince("2017.3.0")]
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

        private AssemblyList<StringStorageDefaultV1> _originalAssemblyNames;

        public List<StringStorageDefaultV1> ManagedAssemblyList = new();
        public int AssemblyCount => ManagedAssemblyList.Count;

        public unsafe void CopyNativeAssemblyListToManaged()
        {
            MonoManagerCommon.CopyNativeAssemblyListToManagedV1(ManagedAssemblyList, _this->m_AssemblyNames);
        }

        public void AddAssembliesToManagedList(List<string> pluginAssemblyPaths)
        {
            MonoManagerCommon.AddAssembliesToManagedListV1(ManagedAssemblyList, pluginAssemblyPaths);
        }

        public unsafe void AllocNativeAssemblyListFromManaged()
        {
            _originalAssemblyNames = _this->m_AssemblyNames;
            MonoManagerCommon.AllocNativeAssemblyListFromManagedV1(ManagedAssemblyList, &_this->m_AssemblyNames);
        }

        public unsafe void PrintAssemblies()
        {
            MonoManagerCommon.PrintAssembliesV1(_this->m_AssemblyNames);
        }

        public unsafe void RestoreOriginalAssemblyNamesArrayPtr()
        {
            _this->m_AssemblyNames = _originalAssemblyNames;
        }
    }
}
