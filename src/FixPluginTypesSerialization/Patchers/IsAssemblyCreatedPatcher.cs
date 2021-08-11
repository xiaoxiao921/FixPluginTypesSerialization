using System;
using System.Runtime.InteropServices;
using FixPluginTypesSerialization.UnityPlayer.Structs;
using FixPluginTypesSerialization.Util;
using MonoMod.RuntimeDetour;

namespace FixPluginTypesSerialization.Patchers
{
    internal unsafe class IsAssemblyCreatedPatcher : Patcher
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool IsAssemblyCreatedDelegate(MonoManager* _this, int index);

        private static IsAssemblyCreatedDelegate original;

        private static NativeDetour _detour;

        protected override BytePattern[] Patterns { get; } =
        {
            "E8 ? ? ? ? 84 C0 74 43 45 84 FF"
        };

        internal static int VanillaAssemblyCount;

        protected override unsafe void Apply(IntPtr from)
        {
            var hookPtr =
                Marshal.GetFunctionPointerForDelegate(new IsAssemblyCreatedDelegate(OnIsAssemblyCreated));

            _detour = new NativeDetour(from, hookPtr, new NativeDetourConfig {ManualApply = true});

            original = _detour.GenerateTrampoline<IsAssemblyCreatedDelegate>();
            _detour.Apply();
        }

        internal static void Dispose()
        {
            _detour.Dispose();
        }

        private static unsafe bool OnIsAssemblyCreated(MonoManager* _this, int index)
        {
            if (index >= VanillaAssemblyCount)
            {
                return true;
            }

            return original(_this, index);
        }
    }
}