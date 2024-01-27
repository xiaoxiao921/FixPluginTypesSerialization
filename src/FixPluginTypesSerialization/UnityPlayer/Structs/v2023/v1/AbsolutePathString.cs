using FixPluginTypesSerialization.Patchers;
using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v2023.v1
{
    [ApplicableToUnityVersionsSince("2023.1.0")]
    public class AbsolutePathString : IAbsolutePathString
    {
        public AbsolutePathString()
        {

        }

        public AbsolutePathString(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer { get; set; }

        private unsafe StringStorageDefaultV3* _this => (StringStorageDefaultV3*)Pointer;

        public unsafe void FixAbsolutePath()
        {
            if (!_this->union.embedded.flags.IsEmbedded && (_this->union.heap.data == 0 || _this->union.heap.size == 0))
            {
                return;
            }

            var pathNameStr = _this->union.embedded.flags.IsEmbedded
                ? Marshal.PtrToStringAnsi((IntPtr)_this->union.embedded.data)
                : Marshal.PtrToStringAnsi(_this->union.heap.data, (int)_this->union.heap.size);
            
            var fileNameStr = Path.GetFileName(pathNameStr);
            var newPathIndex = FixPluginTypesSerializationPatcher.PluginNames.IndexOf(fileNameStr);
            if (newPathIndex == -1)
            {
                return;
            }

            var newPath = FixPluginTypesSerializationPatcher.PluginPaths[newPathIndex];
            var newNativePath = CommonUnityFunctions.MallocString(newPath, UseRightStructs.LabelMemStringId, out var length);
            if (!_this->union.embedded.flags.IsEmbedded)
            {
                CommonUnityFunctions.FreeAllocInternal(_this->union.heap.data, _this->label);
            }

            var str = _this;
            str->union = new StringStorageDefaultV3Union
            {
                heap = new HeapAllocatedRepresentationV3
                {
                    data = newNativePath,
                    capacity = length,
                    size = length,
                    flags = new StringStorageDefaultV3Flags
                    {
                        IsHeap = true
                    }
                }
            };
            str->label = UseRightStructs.LabelMemStringId;
        }

        public unsafe string ToStringAnsi()
        {
            if (!_this->union.embedded.flags.IsEmbedded && (_this->union.heap.data == 0 || _this->union.heap.size == 0))
            {
                return null;
            }

            if (_this->union.embedded.flags.IsEmbedded)
            {
                return Marshal.PtrToStringAnsi((IntPtr)_this->union.embedded.data);
            }
            else
            {
                return Marshal.PtrToStringAnsi(_this->union.heap.data, (int)_this->union.heap.size);
            };
        }
    }
}
