using FixPluginTypesSerialization.Patchers;
using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.Util;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v2019.v4
{
    // core::StringStorageDefault<char> from ida -> produced C header file
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct AssemblyStringStruct
    {
        public const int ValidStringLabel = 0x2B;

        public nint data;
        public ulong capacity;
        public ulong extra;
        public ulong size;
        public int label;

        public bool IsValid() => data != 0 && label == ValidStringLabel;
    }

    [ApplicableToUnityVersionsSince("2019.4.0")]
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
            if (_this->data != 0)
            {
                var pathNameStr = Marshal.PtrToStringAnsi(_this->data, (int)_this->size);

                var newPath = FixPluginTypesSerializationPatcher.PluginPaths.FirstOrDefault(p => Path.GetFileName(p) == Path.GetFileName(pathNameStr));

                if (!string.IsNullOrEmpty(newPath))
                {
                    var length = (ulong)newPath.Length;

                    var newNativePath = Marshal.StringToHGlobalAnsi(newPath);

                    var originalData = new OriginalPathData((IntPtr)_this, _this->data, _this->size);
                    ReadStringFromFile.ModifiedPathsToOriginalPaths.Add(newNativePath, originalData);

                    _this->data = newNativePath;
                    _this->capacity = length;
                    _this->size = length;
                }
            }
        }

        /// <summary>
        /// _this is a const char*.
        /// </summary>
        /// <param name="constCharPtr"></param>
        public unsafe void RestoreOriginalString(IntPtr constCharPtr)
        {
            // So that Unity can call free_alloc_internal on it
            if (ReadStringFromFile.ModifiedPathsToOriginalPaths.TryGetValue(constCharPtr, out var originalData))
            {
                var assemblyString = (AssemblyStringStruct*)originalData.thisRef;
                assemblyString->data = originalData.thisDataRef;
                assemblyString->size = originalData.size;
                assemblyString->capacity = originalData.size;
            }
        }

        public unsafe string ToStringAnsi()
        {
            if (!_this->IsValid())
            {
                return null;
            }

            return Marshal.PtrToStringAnsi(_this->data, (int)_this->size);
        }
    }
}
