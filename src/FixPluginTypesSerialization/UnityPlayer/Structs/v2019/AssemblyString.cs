using FixPluginTypesSerialization.Patchers;
using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v2019
{
    // core::StringStorageDefault<char> from ScriptingManager.h
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct AssemblyStringStruct
    {
        // MemLabelIdentifier::kMemStringId
        public const int ValidStringLabel = 0x45;

        public nint data;
        public ulong capacity;
        public ulong extra;
        public ulong size;
        public int label;

        public bool IsValid() => data != 0 && label == ValidStringLabel;
    }

    [ApplicableToUnityVersionsSince("2019.3.0")]
    public class AssemblyString : IAssemblyString
    {
        public AssemblyString()
        {

        }

        public AssemblyString(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer { get; set; }

        private unsafe AssemblyStringStruct* _this => (AssemblyStringStruct*)Pointer;

        public unsafe void FixAbsolutePath()
        {
            if (_this->IsValid())
            {
                var pathNameStr = Marshal.PtrToStringAnsi(_this->data);

                var newPath = FixPluginTypesSerializationPatcher.PluginPaths.FirstOrDefault(p => Path.GetFileName(p) == Path.GetFileName(pathNameStr));

                if (!string.IsNullOrEmpty(newPath))
                {
                    var length = (ulong)newPath.Length;

                    var newNativePath = Marshal.StringToHGlobalAnsi(newPath);

                    var originalData = ((IntPtr)_this, _this->data);
                    ReadStringFromFile.ModifiedPathsToOriginalPaths.Add(newNativePath, originalData);

                    _this->data = newNativePath;
                    _this->capacity = length;
                    _this->size = length;
                }
            }
        }

        public unsafe void RestoreOriginalString()
        {
            // So that Unity can call free_alloc_internal on it
            if (ReadStringFromFile.ModifiedPathsToOriginalPaths.TryGetValue((IntPtr)_this, out var originalData))
            {
                var assemblyString = (AssemblyStringStruct*)originalData.Item1;
                var originalString = originalData.Item2;
                assemblyString->data = originalString;
            }
        }
    }
}
