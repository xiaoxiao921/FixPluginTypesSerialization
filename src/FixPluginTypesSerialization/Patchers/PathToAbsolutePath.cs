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
    internal unsafe class PathToAbsolutePath : Patcher
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate IntPtr PathToAbsolutePathDelegateV2(IntPtr outData, IntPtr assemblyStringPathName);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate StringStorageDefaultV1 PathToAbsolutePathDelegateV1(IntPtr assemblyStringPathName);

        private static NativeDetour _detourPathToAbsolutePath;
        private static PathToAbsolutePathDelegateV2 originalPathToAbsolutePathV2;
        private static PathToAbsolutePathDelegateV1 originalPathToAbsolutePathV1;

        internal static bool IsApplied { get; private set; }

        protected override BytePattern[] PdbPatterns { get; } =
        {
            Encoding.ASCII.GetBytes(nameof(PathToAbsolutePath))
        };

        protected override BytePattern[] SigPatterns { get; } =
        {
        };

        protected override unsafe void Apply(IntPtr from)
        {
            if ((UseRightStructs.UnityVersion >= new Version(2018, 1) && UseRightStructs.UnityVersion < new Version(2020, 0)) ||
                UseRightStructs.UnityVersion >= new Version(2020, 3))
            {
                var hookPtr = Marshal.GetFunctionPointerForDelegate(new PathToAbsolutePathDelegateV2(OnPathToAbsolutePathV2));
                _detourPathToAbsolutePath = new NativeDetour(from, hookPtr, new NativeDetourConfig { ManualApply = true });
                originalPathToAbsolutePathV2 = _detourPathToAbsolutePath.GenerateTrampoline<PathToAbsolutePathDelegateV2>();
            }
            else
            {
                var hookPtr = Marshal.GetFunctionPointerForDelegate(new PathToAbsolutePathDelegateV1(OnPathToAbsolutePathV1));
                _detourPathToAbsolutePath = new NativeDetour(from, hookPtr, new NativeDetourConfig { ManualApply = true });
                originalPathToAbsolutePathV1 = _detourPathToAbsolutePath.GenerateTrampoline<PathToAbsolutePathDelegateV1>();
            }

            _detourPathToAbsolutePath.Apply();

            IsApplied = true;
        }

        internal static void Dispose()
        {
            if (_detourPathToAbsolutePath != null && _detourPathToAbsolutePath.IsApplied)
            {
                _detourPathToAbsolutePath.Dispose();
            }
            IsApplied = false;
        }

        private static unsafe StringStorageDefaultV1 OnPathToAbsolutePathV1(IntPtr assemblyStringPathName)
        {
            var assemblyString = UseRightStructs.GetStruct<IRelativePathString>(assemblyStringPathName);

            if (assemblyString.CreatePluginAbsolutePath(out var output))
            {
                return output;
            }

            return originalPathToAbsolutePathV1(assemblyStringPathName);
        }

        private static unsafe IntPtr OnPathToAbsolutePathV2(IntPtr outData, IntPtr assemblyStringPathName)
        {
            var assemblyString = UseRightStructs.GetStruct<IRelativePathString>(assemblyStringPathName);

            var str = (StringStorageDefaultV1*)outData;
            if (assemblyString.CreatePluginAbsolutePath(outData))
            {
                return outData;
            }

            return originalPathToAbsolutePathV2(outData, assemblyStringPathName);
        }
    }
}