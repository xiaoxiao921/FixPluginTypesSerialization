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

        protected override BytePattern[] Patterns { get; } =
        {
            "E8 ? ? ? ? 84 C0 74 43 45 84 FF"
        };

        protected override unsafe void Apply(IntPtr from)
        {
            var hookPtr =
                Marshal.GetFunctionPointerForDelegate(new IsAssemblyCreatedDelegate(OnIsAssemblyCreated));

            var det = new NativeDetour(from, hookPtr, new NativeDetourConfig {ManualApply = true});

            original = det.GenerateTrampoline<IsAssemblyCreatedDelegate>();
            det.Apply();
        }

        private static unsafe bool OnIsAssemblyCreated(MonoManager* _this, int index)
        {
            // Todo : Don't hardcode this
            const int vanillaAssemblyCount = 84;
            if (index >= vanillaAssemblyCount)
            {
                return true;
            }

            return original(_this, index);
        }
    }
}