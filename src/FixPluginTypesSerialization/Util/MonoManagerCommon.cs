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
        public static unsafe void CopyNativeAssemblyListToManagedV1(List<StringStorageDefaultV1> managedAssemblyList, Vector<StringStorageDefaultV1> assemblyNames)
        {
            managedAssemblyList.Clear();

            for (StringStorageDefaultV1* s = assemblyNames.first; s != assemblyNames.last; s++)
            {
                managedAssemblyList.Add(*s);
            }
        }

        public static unsafe void AddAssembliesToManagedListV1(List<StringStorageDefaultV1> managedAssemblyList, List<string> pluginAssemblyPaths)
        {
            foreach (var pluginAssemblyPath in pluginAssemblyPaths)
            {
                var pluginAssemblyName = Path.GetFileName(pluginAssemblyPath);
                var length = (ulong)pluginAssemblyName.Length;
                var strPtr = Marshal.StringToHGlobalAnsi(pluginAssemblyName);
                //Ansi string might be longer than managed
                for (var c = (byte*)strPtr + length; *c != 0; c++, length++) { }

                var assemblyString = new StringStorageDefaultV1
                {
                    label = UseRightStructs.LabelMemStringId,
                    data = strPtr,
                    capacity = length,
                    size = length
                };

                managedAssemblyList.Add(assemblyString);
            }
        }

        public static unsafe void AllocNativeAssemblyListFromManagedV1(List<StringStorageDefaultV1> managedAssemblyList, Vector<StringStorageDefaultV1>* assemblyNames)
        {
            var nativeArray = (StringStorageDefaultV1*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(StringStorageDefaultV1)) * managedAssemblyList.Count);

            var i = 0;
            for (StringStorageDefaultV1* s = nativeArray; i < managedAssemblyList.Count; s++, i++)
            {
                *s = managedAssemblyList[i];
            }

            assemblyNames->first = nativeArray;
            assemblyNames->last = nativeArray + managedAssemblyList.Count;
            assemblyNames->end = assemblyNames->last;
        }

        public static unsafe void PrintAssembliesV1(Vector<StringStorageDefaultV1> assemblyNames)
        {
            for (StringStorageDefaultV1* s = assemblyNames.first; s != assemblyNames.last; s++)
            {
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
                managedAssemblyList.Add(*s);
            }
        }

        public static unsafe void AllocNativeAssemblyListFromManagedV2(List<StringStorageDefaultV1> managedAssemblyList, DynamicArrayData* assemblyNames)
        {
            var nativeArray = (StringStorageDefaultV1*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(StringStorageDefaultV1)) * managedAssemblyList.Count);

            var i = 0;
            for (StringStorageDefaultV1* s = nativeArray; i < managedAssemblyList.Count; s++, i++)
            {
                *s = managedAssemblyList[i];
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
                managedAssemblyList.Add(*s);
            }
        }

        public static unsafe void AddAssembliesToManagedListV3(List<StringStorageDefaultV2> managedAssemblyList, List<string> pluginAssemblyPaths)
        {
            foreach (var pluginAssemblyPath in pluginAssemblyPaths)
            {
                var pluginAssemblyName = Path.GetFileName(pluginAssemblyPath);
                var length = (ulong)pluginAssemblyName.Length;
                var strPtr = Marshal.StringToHGlobalAnsi(pluginAssemblyName);
                //Ansi string might be longer than managed
                for (var c = (byte*)strPtr + length; *c != 0; c++, length++) { }

                var assemblyString = new StringStorageDefaultV2
                {
                    union = new StringStorageDefaultV2Union
                    {
                        heap = new HeapAllocatedRepresentationV2
                        {
                            data = strPtr,
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
                *s = managedAssemblyList[i];
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
                    Log.Warning($"Ass: {Marshal.PtrToStringAnsi(s->union.heap.data, (int)s->union.heap.size)} | label : {s->label:X}");
                }
            }
        }

        public static unsafe void CopyNativeAssemblyListToManagedV4(List<StringStorageDefaultV3> managedAssemblyList, DynamicArrayData assemblyNames)
        {
            managedAssemblyList.Clear();

            ulong i = 0;
            for (StringStorageDefaultV3* s = (StringStorageDefaultV3*)assemblyNames.ptr;
                i < assemblyNames.size;
                s++, i++)
            {
                managedAssemblyList.Add(*s);
            }
        }

        public static unsafe void AddAssembliesToManagedListV4(List<StringStorageDefaultV3> managedAssemblyList, List<string> pluginAssemblyPaths)
        {
            foreach (var pluginAssemblyPath in pluginAssemblyPaths)
            {
                var pluginAssemblyName = Path.GetFileName(pluginAssemblyPath);
                var length = (ulong)pluginAssemblyName.Length;
                var strPtr = Marshal.StringToHGlobalAnsi(pluginAssemblyName);
                //Ansi string might be longer than managed
                for (var c = (byte*)strPtr + length; *c != 0; c++, length++) { }

                var assemblyString = new StringStorageDefaultV3
                {
                    union = new StringStorageDefaultV3Union
                    {
                        heap = new HeapAllocatedRepresentationV3
                        {
                            data = strPtr,
                            capacity = length,
                            size = length,
                            flags = new StringStorageDefaultV3Flags
                            {
                                IsHeap = true,
                            }
                        }
                    },
                    label = UseRightStructs.LabelMemStringId,
                };

                managedAssemblyList.Add(assemblyString);
            }
        }

        public static unsafe void AllocNativeAssemblyListFromManagedV4(List<StringStorageDefaultV3> managedAssemblyList, DynamicArrayData* assemblyNames)
        {
            var nativeArray = (StringStorageDefaultV3*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(StringStorageDefaultV3)) * managedAssemblyList.Count);

            var i = 0;
            for (StringStorageDefaultV3* s = nativeArray; i < managedAssemblyList.Count; s++, i++)
            {
                *s = managedAssemblyList[i];
            }

            assemblyNames->ptr = (nint)nativeArray;
            assemblyNames->size = (ulong)managedAssemblyList.Count;
            assemblyNames->capacity = assemblyNames->size;
        }

        public static unsafe void PrintAssembliesV4(DynamicArrayData assemblyNames)
        {
            ulong i = 0;
            for (StringStorageDefaultV3* s = (StringStorageDefaultV3*)assemblyNames.ptr;
                i < assemblyNames.size;
                s++, i++)
            {
                if (s->union.embedded.flags.IsEmbedded)
                {
                    Log.Warning($"Ass: {Marshal.PtrToStringAnsi((IntPtr)s->union.embedded.data)} | label : {s->label:X}");
                }
                else
                {
                    Log.Warning($"Ass: {Marshal.PtrToStringAnsi(s->union.heap.data, (int)s->union.heap.size)} | label : {s->label:X}");
                }
            }
        }
    }
}
