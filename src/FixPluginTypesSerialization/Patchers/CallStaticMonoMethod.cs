using System;
using System.Runtime.InteropServices;
using System.Text;
using FixPluginTypesSerialization.Util;
using MonoMod.RuntimeDetour;

namespace FixPluginTypesSerialization.Patchers
{
    internal unsafe class CallStaticMonoMethod : Patcher
    {
        [UnmanagedFunctionPointer(CallingConvention.FastCall)]
        private delegate bool CallStaticMonoMethodDelegate(IntPtr result, IntPtr className, IntPtr methodName, IntPtr parameters);

        private static NativeDetour _detour;
        private static CallStaticMonoMethodDelegate orig;

        protected override BytePattern[] PdbPatterns { get; } =
        {
            Encoding.ASCII.GetBytes(nameof(CallStaticMonoMethod))
        };

        protected override BytePattern[] SigPatterns { get; } =
        {
            "E8 ? ? ? ? 48 8D 4D 90 E8 ? ? ? ? 48 8D 4D B0 E8 ? ? ? ? 41 8B 17"
        };

        protected override unsafe void Apply(IntPtr from)
        {
            ApplyInternal(from);
        }

        private void ApplyInternal(IntPtr from)
        {
            var hookPtr = Marshal.GetFunctionPointerForDelegate(new CallStaticMonoMethodDelegate(OnCallStaticMonoMethod));
            _detour = new NativeDetour(from, hookPtr, new NativeDetourConfig { ManualApply = true });

            orig = _detour.GenerateTrampoline<CallStaticMonoMethodDelegate>();
            _detour.Apply();
        }

        private static unsafe bool OnCallStaticMonoMethod(IntPtr result, IntPtr className, IntPtr methodName, IntPtr parameters)
        {
            Log.Error("OnCallStaticMonoMethod");

            var res = orig(result, className, methodName, parameters);

            return res;
        }
    }
}