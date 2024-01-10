using System;
using System.Runtime.InteropServices;
using System.Text;
using FixPluginTypesSerialization.Util;
using MonoMod.RuntimeDetour;

namespace FixPluginTypesSerialization.Patchers
{
    internal unsafe class ScriptingManagerDeconstructor : Patcher
    {
        [UnmanagedFunctionPointer(CallingConvention.FastCall)]
        private delegate void ScriptingManagerDeconstructorDelegate(IntPtr scriptingManagerPtr);

        private static NativeDetour _detour;
        private static ScriptingManagerDeconstructorDelegate orig;

        internal static bool IsApplied { get; private set; }

        protected override BytePattern[] PdbPatterns { get; } =
        {
            Encoding.ASCII.GetBytes("ScriptingManager::~ScriptingManager"),
            Encoding.ASCII.GetBytes("?1ScriptingManager@"),
        };

        protected override BytePattern[] SigPatterns { get; } =
        {
            "48 89 5C 24 ? 57 48 83 EC 20 48 8D 05 ? ? ? ? 48 8B D9 48 89 01 48 81 C1 ? ? ? ? E8 ? ? ? ? 48 8D 8B ? ? ? ? E8 ? ? ? ? 48 8D 8B" // 2019.4.16
        };

        protected override unsafe void Apply(IntPtr from)
        {
            ApplyDetour(from);
            IsApplied = true;
        }

        private void ApplyDetour(IntPtr from)
        {
            var hookPtr = Marshal.GetFunctionPointerForDelegate(new ScriptingManagerDeconstructorDelegate(OnDeconstructor));
            _detour = new NativeDetour(from, hookPtr, new NativeDetourConfig { ManualApply = true });

            orig = _detour.GenerateTrampoline<ScriptingManagerDeconstructorDelegate>();
            _detour.Apply();
        }

        internal static void Dispose()
        {
            DisposeDetours();
            IsApplied = false;
        }

        private static void DisposeDetours()
        {
            if (_detour != null && _detour.IsApplied)
            {
                _detour.Dispose();
            }
        }

        private static unsafe void OnDeconstructor(IntPtr scriptingManagerPtr)
        {
            AwakeFromLoad.CurrentMonoManager.RestoreOriginalAssemblyNamesArrayPtr();
            Log.Info("Restored original AssemblyNames list");

            orig(scriptingManagerPtr);
        }
    }
}
