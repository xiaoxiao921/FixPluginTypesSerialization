using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using FixPluginTypesSerialization.UnityPlayer;
using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.Util;
using MonoMod.RuntimeDetour;

namespace FixPluginTypesSerialization.Patchers
{
    internal unsafe class ReadStringFromFile : Patcher
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool ReadStringFromFileDelegate(IntPtr outData, IntPtr assemblyStringPathName);

        private static NativeDetour _detourReadStringFromFile;
        private static ReadStringFromFileDelegate originalReadStringFromFile;

        private static readonly IntPtr Mono = OpenMonoLibrary();
        private static readonly IntPtr mono_assembly_load_from_full_fn = Mono.GetFunction("mono_assembly_load_from_full");

        private delegate IntPtr mono_assembly_load_from_full_delegate(IntPtr image, IntPtr constCharFName, IntPtr status, IntPtr refonly);

        private static NativeDetour _monoDetour;
        private static mono_assembly_load_from_full_delegate originalMonoAssemblyLoadFromFull;

        internal static readonly Dictionary<IntPtr, OriginalPathData> ModifiedPathsToOriginalPaths = new();

        protected override BytePattern[] PdbPatterns { get; } =
        {
            Encoding.ASCII.GetBytes(nameof(ReadStringFromFile))
        };

        protected override BytePattern[] SigPatterns { get; } =
        {
            "E8 ? ? ? ? 4C 8D 45 EF 48 8D 4D 17 84 C0", // 5.0.4
            "E8 ? ? ? ? 48 8B 4D B7 0F B6 F0", // 2018.4.16
            "E8 ? ? ? ? 48 8D 4D 8F 0F B6 D8" // 2019.4.16
        };

        protected override unsafe void Apply(IntPtr from)
        {
            ApplyReadStringFromFileDetour(from);
            ApplyMonoDetour();

            IsApplied = true;
        }

        private void ApplyReadStringFromFileDetour(IntPtr from)
        {
            var hookPtr = Marshal.GetFunctionPointerForDelegate(new ReadStringFromFileDelegate(OnReadStringFromFile));
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

        private static unsafe bool OnReadStringFromFile(IntPtr outData, IntPtr assemblyStringPathName)
        {
            var assemblyString = UseRightStructs.GetStruct<IAssemblyString>(assemblyStringPathName);

            assemblyString.FixAbsolutePath();

            var res = originalReadStringFromFile(outData, assemblyStringPathName);

            return res;
        }

        private static unsafe IntPtr OnMonoAssemblyLoadFromFull(IntPtr image, IntPtr constCharFNamePtr, IntPtr status, IntPtr refonly)
        {
            var res = originalMonoAssemblyLoadFromFull(image, constCharFNamePtr, status, refonly);

            var constCharFName = UseRightStructs.GetStruct<IAssemblyString>(constCharFNamePtr);

            constCharFName.RestoreOriginalString(constCharFNamePtr);

            return res;
        }

        private static IntPtr OpenMonoLibrary()
        {
            var ptr = NativeLibraryHelper.OpenLibrary("mono-2.0-bdwgc.dll");
            if (ptr != IntPtr.Zero)
            {
                return ptr;
            }

            ptr = NativeLibraryHelper.OpenLibrary("mono.dll");

            return ptr;
        }
    }
}