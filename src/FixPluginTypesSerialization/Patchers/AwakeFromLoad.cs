using System;
using System.Runtime.InteropServices;
using System.Text;
using FixPluginTypesSerialization.UnityPlayer;
using FixPluginTypesSerialization.Util;
using MonoMod.RuntimeDetour;

namespace FixPluginTypesSerialization.Patchers
{
    internal unsafe class AwakeFromLoad : Patcher
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void AwakeFromLoadDelegate(IntPtr _monoManager, int awakeMode);

        private static AwakeFromLoadDelegate original;

        private static NativeDetour _detour;

        protected override BytePattern[] Patterns { get; } =
        {
            Encoding.ASCII.GetBytes("MonoManager::" + nameof(AwakeFromLoad))
        };

        protected override unsafe void Apply(IntPtr from)
        {
            var hookPtr =
                Marshal.GetFunctionPointerForDelegate(new AwakeFromLoadDelegate(OnAwakeFromLoad));

            _detour = new NativeDetour(from, hookPtr, new NativeDetourConfig { ManualApply = true });

            original = _detour.GenerateTrampoline<AwakeFromLoadDelegate>();
            _detour.Apply();
        }

        internal static void Dispose()
        {
            _detour.Dispose();
        }

        private static unsafe void OnAwakeFromLoad(IntPtr _monoManager, int awakeMode)
        {
            var monoManager = UseRightStructs.GetMonoManager(_monoManager);

            monoManager.CopyNativeAssemblyListToManaged();

            IsAssemblyCreated.VanillaAssemblyCount = monoManager.AssemblyCount;

            monoManager.AddAssembliesToManagedList(FixPluginTypesSerializationPatcher.PluginPaths);

            monoManager.AllocNativeAssemblyListFromManaged();

            monoManager.PrintAssemblies();

            original(_monoManager, awakeMode);

            // Dispose the ReadStringFromFile detour as we don't need it anymore
            // and could hog resources for nothing otherwise
            ReadStringFromFile.Dispose();
        }
    }
}