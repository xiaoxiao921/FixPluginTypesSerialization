using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using FixPluginTypesSerialization.UnityPlayer;
using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.Util;
using MonoMod.RuntimeDetour;

namespace FixPluginTypesSerialization.Patchers
{
    internal unsafe class IsFileCreated : Patcher
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool IsFileCreatedDelegate(IntPtr str);

        private static IsFileCreatedDelegate original;

        private static NativeDetour _detour;

        internal static bool IsApplied { get; private set; }

        protected override BytePattern[] PdbPatterns { get; } =
        {
            Encoding.ASCII.GetBytes(nameof(IsFileCreated)),
        };

        protected override unsafe void Apply(IntPtr from)
        {
            var hookPtr =
                Marshal.GetFunctionPointerForDelegate(new IsFileCreatedDelegate(OnIsFileCreated));

            _detour = new NativeDetour(from, hookPtr, new NativeDetourConfig { ManualApply = true });

            original = _detour.GenerateTrampoline<IsFileCreatedDelegate>();
            _detour.Apply();

            IsApplied = true;
        }

        internal static void Dispose()
        {
            _detour?.Dispose();
            IsApplied = false;
        }

        private static unsafe bool OnIsFileCreated(IntPtr str)
        {
            var assemblyString = UseRightStructs.GetStruct<IIsFileCreatedParam>(str);
            var actualString = assemblyString.ToStringAnsi();

            if (actualString is not null && FixPluginTypesSerializationPatcher.PluginNames.Any(actualString.EndsWith))
            {
                return true;
            }

            return original(str);
        }
    }
}