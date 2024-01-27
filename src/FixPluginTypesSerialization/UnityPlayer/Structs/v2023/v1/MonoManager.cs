using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.Util;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v2023.v1
{
    [ApplicableToUnityVersionsSince("2023.1.0")]
    public class MonoManager : IMonoManager
    {
        public MonoManager()
        {
        }

        public MonoManager(IntPtr pointer)
        {
        }

        public IntPtr Pointer { get => CommonUnityFunctions.ScriptingAssemblies; set { } }

        private unsafe RuntimeStatic<ScriptingAssemblies>* _this => (RuntimeStatic<ScriptingAssemblies>*)Pointer;

        private ScriptingAssemblies _originalScriptingAssemblies;

        public List<StringStorageDefaultV3> ManagedAssemblyList = new();
        public int AssemblyCount => ManagedAssemblyList.Count;

        public unsafe void CopyNativeAssemblyListToManaged()
        {
            MonoManagerCommon.CopyNativeAssemblyListToManagedV4(ManagedAssemblyList, _this->value->names);
        }

        public void AddAssembliesToManagedList(List<string> pluginAssemblyPaths)
        {
            MonoManagerCommon.AddAssembliesToManagedListV4(ManagedAssemblyList, pluginAssemblyPaths);
        }

        public unsafe void AllocNativeAssemblyListFromManaged()
        {
            MonoManagerCommon.AllocNativeAssemblyListFromManagedV4(ManagedAssemblyList, &_this->value->names);
        }

        public unsafe void PrintAssemblies()
        {
            MonoManagerCommon.PrintAssembliesV4(_this->value->names);
        }

        public unsafe void RestoreOriginalAssemblyNamesArrayPtr()
        {
            *_this->value = _originalScriptingAssemblies;
        }
    }
}
