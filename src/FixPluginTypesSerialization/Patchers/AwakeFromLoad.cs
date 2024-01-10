using System;
using System.Runtime.InteropServices;
using System.Text;
using FixPluginTypesSerialization.UnityPlayer;
using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
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

        internal static IMonoManager CurrentMonoManager;
        internal static bool IsApplied { get; private set; }

        protected override BytePattern[] PdbPatterns { get; } =
        {
            Encoding.ASCII.GetBytes("MonoManager::" + nameof(AwakeFromLoad)),
            Encoding.ASCII.GetBytes(nameof(AwakeFromLoad) + "@MonoManager")
        };

        protected override BytePattern[] SigPatterns { get; } =
        {
            "40 53 48 81 EC ? ? ? ? 33 C0 C7 44 24 ? ? ? ? ? 0F 57 C0" // 2018.4.16 and 2019.4.16
        };

        protected override unsafe void Apply(IntPtr from)
        {
            var hookPtr =
                Marshal.GetFunctionPointerForDelegate(new AwakeFromLoadDelegate(OnAwakeFromLoad));

            _detour = new NativeDetour(from, hookPtr, new NativeDetourConfig { ManualApply = true });

            original = _detour.GenerateTrampoline<AwakeFromLoadDelegate>();
            _detour.Apply();

            IsApplied = true;
        }

        internal static void Dispose()
        {
            _detour?.Dispose();
            IsApplied = false;
        }

        private static unsafe void OnAwakeFromLoad(IntPtr _monoManager, int awakeMode)
        {
            if (!ReadStringFromFile.IsApplied)
            {
                CurrentMonoManager = UseRightStructs.GetStruct<IMonoManager>(_monoManager);

                CurrentMonoManager.CopyNativeAssemblyListToManaged();

                IsAssemblyCreated.VanillaAssemblyCount = CurrentMonoManager.AssemblyCount;

                CurrentMonoManager.AddAssembliesToManagedList(FixPluginTypesSerializationPatcher.PluginPaths);

                CurrentMonoManager.AllocNativeAssemblyListFromManaged();

                //CurrentMonoManager.PrintAssemblies();
            }

            original(_monoManager, awakeMode);

            // Dispose detours as we don't need them anymore
            // and could hog resources for nothing otherwise
            ReadStringFromFile.Dispose();
            IsFileCreated.Dispose();
            PathToAbsolutePath.Dispose();
            IsAssemblyCreated.Dispose();
        }
    }
}