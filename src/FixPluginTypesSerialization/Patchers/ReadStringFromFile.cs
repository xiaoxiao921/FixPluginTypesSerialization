using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using FixPluginTypesSerialization.UnityPlayer.Structs;
using FixPluginTypesSerialization.Util;
using MonoMod.RuntimeDetour;

namespace FixPluginTypesSerialization.Patchers
{
    internal unsafe class ReadStringFromFile : Patcher
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool ReadStringFromFileDelegate(IntPtr outData, AssemblyString* pathName);

        private static NativeDetour _detourReadStringFromFile;
        private static ReadStringFromFileDelegate originalReadStringFromFile;

        private static readonly IntPtr Mono = NativeHelper.OpenLibrary("mono-2.0-bdwgc.dll");
        private static readonly IntPtr mono_assembly_load_from_full_fn = Mono.GetFunction("mono_assembly_load_from_full");

        private delegate IntPtr mono_assembly_load_from_full_delegate(IntPtr image, IntPtr constCharFName, IntPtr status, IntPtr refonly);

        private static NativeDetour _monoDetour;
        private static mono_assembly_load_from_full_delegate originalMonoAssemblyLoadFromFull;

        private static readonly Dictionary<IntPtr, (IntPtr, IntPtr)> ModifiedPathsToOriginalPaths = new();

        protected override BytePattern[] Patterns { get; } =
        {
            Encoding.ASCII.GetBytes(nameof(ReadStringFromFile))
        };

        protected override unsafe void Apply(IntPtr from)
        {
            ApplyReadStringFromFileDetour(from);
            ApplyMonoDetour();
        }

        private void ApplyReadStringFromFileDetour(IntPtr from)
        {
            var hookPtr = Marshal.GetFunctionPointerForDelegate(new ReadStringFromFileDelegate(OnReadFromFile));
            _detourReadStringFromFile = new NativeDetour(from, hookPtr, new NativeDetourConfig { ManualApply = true });

            originalReadStringFromFile = _detourReadStringFromFile.GenerateTrampoline<ReadStringFromFileDelegate>();
            _detourReadStringFromFile.Apply();
        }

        private void ApplyMonoDetour()
        {
            var hookPtr = Marshal.GetFunctionPointerForDelegate(new mono_assembly_load_from_full_delegate(OnMonoAssemblyLoadFromFull));
            _monoDetour = new NativeDetour(mono_assembly_load_from_full_fn, hookPtr, new NativeDetourConfig { ManualApply = true });

            originalMonoAssemblyLoadFromFull = _monoDetour.GenerateTrampoline<mono_assembly_load_from_full_delegate>();
            _monoDetour.Apply();
        }

        internal static void Dispose()
        {
            DisposeDetours();
        }

        private static void DisposeDetours()
        {
            if (_detourReadStringFromFile != null && _detourReadStringFromFile.IsApplied)
            {
                _detourReadStringFromFile.Dispose();
            }

            if (_monoDetour != null && _monoDetour.IsApplied)
            {
                _monoDetour.Dispose();
            }

            // Free the allocated paths from FixAbsolutePath
            foreach (var (allocatedPath, _) in ModifiedPathsToOriginalPaths)
            {
                Marshal.FreeHGlobal(allocatedPath);
            }
        }

        private static unsafe void FixAbsolutePath(ref AssemblyString* pathName)
        {
            if (pathName->IsValid())
            {
                var pathNameStr = Marshal.PtrToStringAnsi(pathName->data);

                var newPath = FixPluginTypesSerializationPatcher.PluginPaths.FirstOrDefault(p => Path.GetFileName(p) == Path.GetFileName(pathNameStr));

                if (!string.IsNullOrEmpty(newPath))
                {
                    var length = (ulong)newPath.Length;

                    var newNativePath = Marshal.StringToHGlobalAnsi(newPath);

                    var originalData = ((IntPtr)pathName, pathName->data);
                    ModifiedPathsToOriginalPaths.Add(newNativePath, originalData);

                    pathName->data = newNativePath;
                    pathName->capacity = length;
                    pathName->size = length;
                }
            }
        }

        private static unsafe bool OnReadFromFile(IntPtr outData, AssemblyString* pathName)
        {
            FixAbsolutePath(ref pathName);

            var res = originalReadStringFromFile(outData, pathName);

            return res;
        }

        private static unsafe IntPtr OnMonoAssemblyLoadFromFull(IntPtr image, IntPtr constCharFName, IntPtr status, IntPtr refonly)
        {
            var res = originalMonoAssemblyLoadFromFull(image, constCharFName, status, refonly);

            RestoreOriginalString(constCharFName);

            return res;
        }

        private static void RestoreOriginalString(IntPtr potentialModifiedPath)
        {
            // So that Unity can call free_alloc_internal on it
            if (ModifiedPathsToOriginalPaths.TryGetValue(potentialModifiedPath, out var originalData))
            {
                var assemblyString = (AssemblyString*)originalData.Item1;
                var originalString = originalData.Item2;
                assemblyString->data = originalString;
            }
        }
    }
}