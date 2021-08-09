using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using FixPluginTypesSerialization.UnityPlayer.Structs;
using FixPluginTypesSerialization.Util;
using MonoMod.RuntimeDetour;

namespace FixPluginTypesSerialization.Patchers
{
    internal unsafe class ReadFromFilePatcher : Patcher
    {
        private static ReadFromFileDelegate original;

        internal static NativeDetour Detour;

        protected override BytePattern[] Patterns { get; } =
        {
            "E8 ? ? ? ? 48 8B 4D B7 0F B6 F0"
        };

        protected override unsafe void Apply(IntPtr from)
        {
            var hookPtr =
                Marshal.GetFunctionPointerForDelegate(new ReadFromFileDelegate(OnReadFromFile));

            Detour = new NativeDetour(from, hookPtr, new NativeDetourConfig {ManualApply = true});

            original = Detour.GenerateTrampoline<ReadFromFileDelegate>();
            Detour.Apply();
        }

        private static unsafe void FixAbsolutePath(ref AssemblyString* pathName)
        {
            if (pathName->IsValid())
            {
                var pathNameStr = Marshal.PtrToStringAnsi(pathName->data);

                var newPath = FixPluginTypesSerializationPatcher.PluginPaths.FirstOrDefault(p => Path.GetFileName(p) == Path.GetFileName(pathNameStr));

                if (!string.IsNullOrEmpty(newPath))
                {
                    var length = (ulong)newPath.Length;

                    var assemblyString = new AssemblyString
                    {
                        label = AssemblyString.ValidStringLabel,
                        data = Marshal.StringToHGlobalAnsi(newPath),
                        capacity = length,
                        size = length
                    };

                    var nativeAlloc = (AssemblyString*)Marshal.AllocHGlobal(Marshal.SizeOf<AssemblyString>());

                    Marshal.StructureToPtr(assemblyString, (IntPtr)nativeAlloc, false);

                    pathName = nativeAlloc;
                }
            }
        }

        private static unsafe bool OnReadFromFile(IntPtr outData, AssemblyString* pathName)
        {
            FixAbsolutePath(ref pathName);
            
            var res = original(outData, pathName);

            return res;
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool ReadFromFileDelegate(IntPtr outData, AssemblyString* pathName);
    }
}