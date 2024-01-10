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

        internal static bool IsApplied { get; private set; }

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
            if (UseRightStructs.UnityVersion < new Version(2020, 2))
            {
                return;
            }

            var hookPtr = Marshal.GetFunctionPointerForDelegate(new ReadStringFromFileDelegate(OnReadStringFromFile));
            _detourReadStringFromFile = new NativeDetour(from, hookPtr, new NativeDetourConfig { ManualApply = true });

            originalReadStringFromFile = _detourReadStringFromFile.GenerateTrampoline<ReadStringFromFileDelegate>();
            _detourReadStringFromFile.Apply();

            IsApplied = true;
        }

        internal static void Dispose()
        {
            if (_detourReadStringFromFile != null && _detourReadStringFromFile.IsApplied)
            {
                _detourReadStringFromFile.Dispose();
            }
            IsApplied = false;
        }

        private static unsafe bool OnReadStringFromFile(IntPtr outData, IntPtr assemblyStringPathName)
        {
            var res = originalReadStringFromFile(outData, assemblyStringPathName);

            if (res)
            {
                var assemblyString = UseRightStructs.GetStruct<IRelativePathString>(assemblyStringPathName);
                assemblyString.AppendToScriptingAssemblies(outData, FixPluginTypesSerializationPatcher.PluginNames);
            }

            return res;
        }
    }
}