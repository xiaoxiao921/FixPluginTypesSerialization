using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using FixPluginTypesSerialization.UnityPlayer;
using FixPluginTypesSerialization.UnityPlayer.Structs.Default;

namespace FixPluginTypesSerialization.Util
{
    internal static class MonoManagerCommon
    {
        public static unsafe void CopyNativeAssemblyListToManagedV1(List<StringStorageDefaultV1> managedAssemblyList, AssemblyList<StringStorageDefaultV1> assemblyNames)
        {
            managedAssemblyList.Clear();

            for (StringStorageDefaultV1* s = assemblyNames.first; s != assemblyNames.last; s++)
            {
                var newAssemblyString = new StringStorageDefaultV1
                {
                    capacity = s->capacity,
                    extra = s->extra,
                    label = s->label,
                    size = s->size,
                    data = s->data
                };

                managedAssemblyList.Add(newAssemblyString);
            }
        }

        public static void AddAssembliesToManagedListV1(List<StringStorageDefaultV1> managedAssemblyList, List<string> pluginAssemblyPaths)
        {
            foreach (var pluginAssemblyPath in pluginAssemblyPaths)
            {
                var pluginAssemblyName = Path.GetFileName(pluginAssemblyPath);
                var length = (ulong)pluginAssemblyName.Length;

                var assemblyString = new StringStorageDefaultV1
                {
                    label = UseRightStructs.LabelMemStringId,
                    data = Marshal.StringToHGlobalAnsi(pluginAssemblyName),
                    capacity = length,
                    size = length
                };

                managedAssemblyList.Add(assemblyString);
            }
        }

        public static unsafe void AllocNativeAssemblyListFromManagedV1(List<StringStorageDefaultV1> managedAssemblyList, AssemblyList<StringStorageDefaultV1>* assemblyNames, out AssemblyList<StringStorageDefaultV1> originalAssemblyNames)
        {
            var nativeArray = (StringStorageDefaultV1*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(StringStorageDefaultV1)) * managedAssemblyList.Count);

            var i = 0;
            for (StringStorageDefaultV1* s = nativeArray; i < managedAssemblyList.Count; s++, i++)
            {
                s->label = managedAssemblyList[i].label;
                s->size = managedAssemblyList[i].size;
                s->capacity = managedAssemblyList[i].capacity;
                s->extra = managedAssemblyList[i].extra;
                s->data = managedAssemblyList[i].data;
            }

            originalAssemblyNames = assemblyNames[0];

            assemblyNames->first = nativeArray;
            assemblyNames->last = nativeArray + managedAssemblyList.Count;
            assemblyNames->end = assemblyNames->last;
        }

        public static unsafe void PrintAssembliesV1(AssemblyList<StringStorageDefaultV1> assemblyNames)
        {
            for (StringStorageDefaultV1* s = assemblyNames.first; s != assemblyNames.last; s++)
            {
                if (s->size == 0)
                {
                    continue;
                }

                var data = s->data;
                if (s->data == 0)
                {
                    data = (nint)s + 8;
                }

                Log.Warning($"Ass: {Marshal.PtrToStringAnsi(data, (int)s->size)} | label : {s->label:X}");
            }
        }

        public static unsafe void CopyNativeAssemblyListToManagedV2(List<StringStorageDefaultV1> managedAssemblyList, DynamicArrayData assemblyNames)
        {
            managedAssemblyList.Clear();

            ulong i = 0;
            for (StringStorageDefaultV1* s = (StringStorageDefaultV1*)assemblyNames.ptr;
                i < assemblyNames.size;
                s++, i++)
            {
                var newAssemblyString = new StringStorageDefaultV1
                {
                    capacity = s->capacity,
                    extra = s->extra,
                    label = s->label,
                    size = s->size,
                    data = s->data
                };

                managedAssemblyList.Add(newAssemblyString);
            }
        }

        public static unsafe void AllocNativeAssemblyListFromManagedV2(List<StringStorageDefaultV1> managedAssemblyList, DynamicArrayData* assemblyNames, out DynamicArrayData originalAssemblyNames)
        {
            var nativeArray = (StringStorageDefaultV1*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(StringStorageDefaultV1)) * managedAssemblyList.Count);

            var i = 0;
            for (StringStorageDefaultV1* s = nativeArray; i < managedAssemblyList.Count; s++, i++)
            {
                s->label = managedAssemblyList[i].label;
                s->size = managedAssemblyList[i].size;
                s->capacity = managedAssemblyList[i].capacity;
                s->extra = managedAssemblyList[i].extra;
                s->data = managedAssemblyList[i].data;
            }

            originalAssemblyNames = assemblyNames[0];

            assemblyNames->ptr = (nint)nativeArray;
            assemblyNames->size = (ulong)managedAssemblyList.Count;
            assemblyNames->capacity = assemblyNames->size;
        }

        public static unsafe void PrintAssembliesV2(DynamicArrayData assemblyNames)
        {
            ulong i = 0;
            for (StringStorageDefaultV1* s = (StringStorageDefaultV1*)assemblyNames.ptr;
                i < assemblyNames.size;
                s++, i++)
            {
                if (s->size == 0)
                {
                    continue;
                }

                var data = s->data;
                if (s->data == 0)
                {
                    data = (nint)s + 8;
                }

                Log.Warning($"Ass: {Marshal.PtrToStringAnsi(data, (int)s->size)} | label : {s->label:X}");
            }
        }
    }
}
