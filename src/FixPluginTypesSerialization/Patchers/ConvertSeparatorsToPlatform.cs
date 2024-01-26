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
    internal unsafe class ConvertSeparatorsToPlatform : Patcher
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void ConvertSeparatorsToPlatformDelegate(IntPtr assemblyStringPathName);

        private static NativeDetour _detourConvertSeparatorsToPlatform;
        private static ConvertSeparatorsToPlatformDelegate originalConvertSeparatorsToPlatform;

        internal static bool IsApplied { get; private set; }

        protected override BytePattern[] PdbPatterns { get; } =
        {
            Encoding.ASCII.GetBytes(nameof(ConvertSeparatorsToPlatform))
        };

        protected override unsafe void Apply(IntPtr from)
        {
            var hookPtr = Marshal.GetFunctionPointerForDelegate(new ConvertSeparatorsToPlatformDelegate(OnConvertSeparatorsToPlatformV1));
            _detourConvertSeparatorsToPlatform = new NativeDetour(from, hookPtr, new NativeDetourConfig { ManualApply = true });
            originalConvertSeparatorsToPlatform = _detourConvertSeparatorsToPlatform.GenerateTrampoline<ConvertSeparatorsToPlatformDelegate>();

            _detourConvertSeparatorsToPlatform.Apply();

            IsApplied = true;
        }

        internal static void Dispose()
        {
            if (_detourConvertSeparatorsToPlatform != null && _detourConvertSeparatorsToPlatform.IsApplied)
            {
                _detourConvertSeparatorsToPlatform.Dispose();
            }
            IsApplied = false;
        }

        private static unsafe void OnConvertSeparatorsToPlatformV1(IntPtr assemblyStringPathName)
        {
            var assemblyString = UseRightStructs.GetStruct<IAbsolutePathString>(assemblyStringPathName);

            assemblyString.FixAbsolutePath();

            originalConvertSeparatorsToPlatform(assemblyStringPathName);
        }
    }
}