using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using FixPluginTypesSerialization.UnityPlayer.Structs;
using FixPluginTypesSerialization.Util;
using MonoMod.RuntimeDetour;

namespace FixPluginTypesSerialization.Patchers
{
    internal unsafe class AddAssembliesPatcher : Patcher
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void AwakeFromLoadDelegate(MonoManager* _this, int awakeMode);

        private static AwakeFromLoadDelegate original;

        protected override BytePattern[] Patterns { get; } =
        {
            "40 53 48 81 EC ? ? ? ? 33 C0 C7 44 24 ? ? ? ? ? 0F 57 C0"
        };

        protected override unsafe void Apply(IntPtr from)
        {
            var hookPtr =
                Marshal.GetFunctionPointerForDelegate(new AwakeFromLoadDelegate(OnAwakeFromLoad));

            var det = new NativeDetour(from, hookPtr, new NativeDetourConfig { ManualApply = true });

            original = det.GenerateTrampoline<AwakeFromLoadDelegate>();
            det.Apply();
        }

        private static unsafe List<AssemblyString> CopyExistingAssemblyList(ref AssemblyList nativeAssemblyList)
        {
            var managedAssemblyList = new List<AssemblyString>();

            for (AssemblyString* s = nativeAssemblyList.first; s != nativeAssemblyList.last; s++)
            {
                var newAssemblyString = new AssemblyString
                {
                    capacity = s->capacity,
                    extra = s->extra,
                    label = s->label,
                    size = s->size,
                    data = Marshal.StringToHGlobalAnsi(s->IsValid() ? Marshal.PtrToStringAnsi(s->data, (int)s->size) : null)
                };

                managedAssemblyList.Add(newAssemblyString);
            }

            return managedAssemblyList;
        }

        private static void AddOurAssemblies(List<AssemblyString> managedAssemblyList, List<string> pluginAssemblyPaths)
        {
            foreach (var pluginAssemblyPath in pluginAssemblyPaths)
            {
                var pluginAssemblyName = Path.GetFileName(pluginAssemblyPath);
                var length = (ulong)pluginAssemblyName.Length;

                var assemblyString = new AssemblyString
                {
                    label = AssemblyString.ValidStringLabel,
                    data = Marshal.StringToHGlobalAnsi(pluginAssemblyName),
                    capacity = length,
                    size = length
                };

                managedAssemblyList.Add(assemblyString);
            }
        }

        private static unsafe void NewNativeAssemblyList(ref AssemblyList nativeAssemblyList, List<AssemblyString> managedAssemblyList)
        {
            var nativeArray = (AssemblyString*)Marshal.AllocHGlobal(Marshal.SizeOf<AssemblyString>() * managedAssemblyList.Count);

            var i = 0;
            for (AssemblyString* s = nativeArray; i < managedAssemblyList.Count; s++, i++)
            {
                s->label = managedAssemblyList[i].label;
                s->size = managedAssemblyList[i].size;
                s->capacity = managedAssemblyList[i].capacity;
                s->extra = managedAssemblyList[i].extra;
                s->data = managedAssemblyList[i].data;
            }

            nativeAssemblyList.first = nativeArray;
            nativeAssemblyList.last = nativeArray + managedAssemblyList.Count;
            nativeAssemblyList.end = nativeAssemblyList.last;
        }

        private static unsafe void PrintAssemblies(ref AssemblyList assemblyNames)
        {
            for (AssemblyString* s = assemblyNames.first; s != assemblyNames.last; s++)
            {
                if (!s->IsValid())
                    continue;

                Log.LogWarning($"Ass: {Marshal.PtrToStringAnsi(s->data, (int)s->size)}");
            }
        }

        private static unsafe void OnAwakeFromLoad(MonoManager* _this, int awakeMode)
        {
            var managedAssemblyList = CopyExistingAssemblyList(ref _this->m_AssemblyNames);

            AddOurAssemblies(managedAssemblyList, FixPluginTypesSerializationPatcher.PluginPaths);

            NewNativeAssemblyList(ref _this->m_AssemblyNames, managedAssemblyList);

            //PrintAssemblies(ref _this->m_AssemblyNames);

            original(_this, awakeMode);

            // Dispose the ReadFromFile detour as we don't need it anymore
            // and could hog resources for nothing otherwise
            if (ReadFromFilePatcher.Detour != null && ReadFromFilePatcher.Detour.IsApplied)
                ReadFromFilePatcher.Detour.Dispose();
        }
    }
}