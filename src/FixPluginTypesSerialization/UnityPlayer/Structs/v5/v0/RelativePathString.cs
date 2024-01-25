using FixPluginTypesSerialization.Patchers;
using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v5.v0
{
    [ApplicableToUnityVersionsSince("3.4.0")]
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

        private unsafe StringStorageDefaultV0* _this => (StringStorageDefaultV0*)Pointer;

        public unsafe void FixAbsolutePath()
        {
            var pathNameStr = _this->flags < 16
                ? Marshal.PtrToStringAnsi(Pointer)
                : Marshal.PtrToStringAnsi(_this->data, (int)_this->size);

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
                CommonUnityFunctions.FreeAllocInternal(_this->data, UseRightStructs.LabelMemStringId);
            }

            var str = _this;
            str->data = newNativePath;
            str->extra1 = (ulong)length;
            str->size = length;
            str->flags = 31;
            str->extra2 = 0;
        }
        
        public unsafe string ToStringAnsi()
        {
            if (_this->flags < 16)
            {
                return Marshal.PtrToStringAnsi(Pointer);
            }

            return Marshal.PtrToStringAnsi(_this->data, (int)_this->size);
        }
    }
}
