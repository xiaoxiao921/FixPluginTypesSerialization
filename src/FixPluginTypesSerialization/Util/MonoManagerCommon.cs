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

        public static unsafe void AllocNativeAssemblyListFromManagedV1(List<StringStorageDefaultV1> managedAssemblyList, AssemblyList<StringStorageDefaultV1>* assemblyNames)
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

        public static unsafe void AllocNativeAssemblyListFromManagedV2(List<StringStorageDefaultV1> managedAssemblyList, DynamicArrayData* assemblyNames)
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

        public static unsafe void CopyNativeAssemblyListToManagedV3(List<StringStorageDefaultV2> managedAssemblyList, DynamicArrayData assemblyNames)
        {
            managedAssemblyList.Clear();

            ulong i = 0;
            for (StringStorageDefaultV2* s = (StringStorageDefaultV2*)assemblyNames.ptr;
                i < assemblyNames.size;
                s++, i++)
            {
                var newAssemblyString = new StringStorageDefaultV2
                {
                    union = s->union,
                    data_repr = s->data_repr,
                    label = s->label,
                };

                managedAssemblyList.Add(newAssemblyString);
            }
        }

        public static void AddAssembliesToManagedListV3(List<StringStorageDefaultV2> managedAssemblyList, List<string> pluginAssemblyPaths)
        {
            foreach (var pluginAssemblyPath in pluginAssemblyPaths)
            {
                var pluginAssemblyName = Path.GetFileName(pluginAssemblyPath);
                var length = (ulong)pluginAssemblyName.Length;

                var assemblyString = new StringStorageDefaultV2
                {
                    union = new StringStorageDefaultV2Union
                    {
                        heap = new HeapAllocatedRepresentation
                        {
                            data = Marshal.StringToHGlobalAnsi(pluginAssemblyName),
                            capacity = length,
                            size = length,
                        }
                    },
                    data_repr = StringRepresentation.Heap,
                    label = UseRightStructs.LabelMemStringId,
                };

                managedAssemblyList.Add(assemblyString);
            }
        }

        public static unsafe void AllocNativeAssemblyListFromManagedV3(List<StringStorageDefaultV2> managedAssemblyList, DynamicArrayData* assemblyNames)
        {
            var nativeArray = (StringStorageDefaultV2*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(StringStorageDefaultV2)) * managedAssemblyList.Count);

            var i = 0;
            for (StringStorageDefaultV2* s = nativeArray; i < managedAssemblyList.Count; s++, i++)
            {
                s->union = managedAssemblyList[i].union;
                s->data_repr = managedAssemblyList[i].data_repr;
                s->label = managedAssemblyList[i].label;
            }

            assemblyNames->ptr = (nint)nativeArray;
            assemblyNames->size = (ulong)managedAssemblyList.Count;
            assemblyNames->capacity = assemblyNames->size;
        }

        public static unsafe void PrintAssembliesV3(DynamicArrayData assemblyNames)
        {
            ulong i = 0;
            for (StringStorageDefaultV2* s = (StringStorageDefaultV2*)assemblyNames.ptr;
                i < assemblyNames.size;
                s++, i++)
            {
                if (s->data_repr == StringRepresentation.Embedded)
                {
                    Log.Warning($"Ass: {Marshal.PtrToStringAnsi((IntPtr)s->union.embedded.data)} | label : {s->label:X}");
                }
                else
                {
                    if (s->union.heap.size == 0)
                    {
                        continue;
                    }

                    Log.Warning($"Ass: {Marshal.PtrToStringAnsi(s->union.heap.data, (int)s->union.heap.size)} | label : {s->label:X}");
                }
            }
        }
    }
}
