using FixPluginTypesSerialization.Patchers;
using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FixPluginTypesSerialization.UnityPlayer.v5.v6.v1
{
    [ApplicableToUnityVersionsSince("5.6.0")]
    public class RelativePathString : IRelativePathString
    {
        public RelativePathString()
        {

        }

        public RelativePathString(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer { get; set; }

        private unsafe StringStorageDefaultV1* _this => (StringStorageDefaultV1*)Pointer;

        public unsafe void FixAbsolutePath()
        {
            if (_this->size == 0)
            {
                return;
            }

            var data = _this->data;
            if (data == 0)
            {
                data = (IntPtr)(Pointer.ToInt64() + 8);
            }

            var pathNameStr = Marshal.PtrToStringAnsi(data, (int)_this->size);

            var fileNameStr = Path.GetFileName(pathNameStr);
            var newPathIndex = FixPluginTypesSerializationPatcher.PluginNames.IndexOf(fileNameStr);
            if (newPathIndex == -1)
            {
                return;
            }

            var newPath = FixPluginTypesSerializationPatcher.PluginPaths[newPathIndex];
            var newNativePath = CommonUnityFunctions.MallocString(newPath, UseRightStructs.LabelMemStringId, out var length);

            if (_this->data != 0)
            {
                CommonUnityFunctions.FreeAllocInternal(_this->data, _this->label);
            }

            var str = _this;
            str->data = newNativePath;
            str->capacity = length;
            str->extra = 0;
            str->size = length;
            str->label = UseRightStructs.LabelMemStringId;
        }
        
        public unsafe string ToStringAnsi()
        {
            if (_this->size == 0)
            {
                return null;
            }

            var data = _this->data;
            if (data == 0)
            {
                data = (nint)_this + 8;
            }

            return Marshal.PtrToStringAnsi(data, (int)_this->size);
        }
    }
}
