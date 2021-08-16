using System;
using System.Runtime.InteropServices;
using System.Text;
using FixPluginTypesSerialization.UnityPlayer.Structs;
using FixPluginTypesSerialization.Util;
using MonoMod.RuntimeDetour;

namespace FixPluginTypesSerialization.Patchers
{
    internal unsafe class IsAssemblyCreated : Patcher
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool IsAssemblyCreatedDelegate(MonoManager* _this, int index);

        private static IsAssemblyCreatedDelegate original;

        private static NativeDetour _detour;

        protected override BytePattern[] Patterns { get; } =
        {
            Encoding.ASCII.GetBytes(nameof(MonoManager) + "::" + nameof(IsAssemblyCreated))
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