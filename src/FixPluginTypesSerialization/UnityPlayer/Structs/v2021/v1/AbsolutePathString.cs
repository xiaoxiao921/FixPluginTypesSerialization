using FixPluginTypesSerialization.Patchers;
using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v2021.v1
{
    [ApplicableToUnityVersionsSince("2021.1.0")]
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

        private unsafe StringStorageDefaultV2* _this => (StringStorageDefaultV2*)Pointer;

        public unsafe void FixAbsolutePath()
        {
            if (_this->data_repr != StringRepresentation.Embedded && (_this->union.heap.data == 0 || _this->union.heap.size == 0))
            {
                return;
            }

            var pathNameStr = _this->data_repr switch
            {
                StringRepresentation.Embedded => Marshal.PtrToStringAnsi((IntPtr)_this->union.embedded.data),
                _ => Marshal.PtrToStringAnsi(_this->union.heap.data, (int)_this->union.heap.size),
            };

            var fileNameStr = Path.GetFileName(pathNameStr);
            var newPathIndex = FixPluginTypesSerializationPatcher.PluginNames.IndexOf(fileNameStr);
            if (newPathIndex == -1)
            {
                return;
            }

            var newPath = FixPluginTypesSerializationPatcher.PluginPaths[newPathIndex];
            var newNativePath = CommonUnityFunctions.MallocString(newPath, UseRightStructs.LabelMemStringId, out var length);
            if (_this->data_repr != StringRepresentation.Embedded)
            {
                CommonUnityFunctions.FreeAllocInternal(_this->union.heap.data, _this->label);
            }
            var str = _this;
            str->union = new StringStorageDefaultV2Union
            {
                heap = new HeapAllocatedRepresentationV2
                {
                    data = newNativePath,
                    capacity = length,
                    size = length
                }
            };
            str->data_repr = StringRepresentation.Heap;
            str->label = UseRightStructs.LabelMemStringId;
        }

        public unsafe string ToStringAnsi()
        {
            if (_this->data_repr != StringRepresentation.Embedded && (_this->union.heap.data == 0 || _this->union.heap.size == 0))
            {
                return null;
            }

            return _this->data_repr switch
            {
                StringRepresentation.Embedded => Marshal.PtrToStringAnsi((IntPtr)_this->union.embedded.data),
                _ => Marshal.PtrToStringAnsi(_this->union.heap.data, (int)_this->union.heap.size),
            };
        }
    }
}
