using FixPluginTypesSerialization.Patchers;
using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v2020.v2
{
    [ApplicableToUnityVersionsSince("2020.2.0")]
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

        private unsafe BasicStringRef* _this => (BasicStringRef*)Pointer;

        public unsafe bool CreatePluginAbsolutePath(out StringStorageDefaultV1 output)
        {
            var fixedOutput = new StringStorageDefaultV1();

            var result = CreatePluginAbsolutePath((IntPtr)(&fixedOutput));
            output = fixedOutput;
            return result;
        }

        public unsafe bool CreatePluginAbsolutePath(IntPtr output)
        {
            if (_this->data == 0 || _this->size == 0)
            {
                return false;
            }

            var pathNameStr = Marshal.PtrToStringAnsi(_this->data, (int)_this->size);
            var newPath = FixPluginTypesSerializationPatcher.PluginPaths.FirstOrDefault(p => Path.GetFileName(p) == Path.GetFileName(pathNameStr));

            if (string.IsNullOrEmpty(newPath))
            {
                return false;
            }

            var newNativePath = CommonUnityFunctions.MallocString(newPath, UseRightStructs.LabelMemStringId, out var length);
            
            var str = (StringStorageDefaultV1*)output;
            str->data = newNativePath;
            str->capacity = length;
            str->extra = 0;
            str->size = length;
            str->label = UseRightStructs.LabelMemStringId;

            return true;
        }

        public unsafe string ToStringAnsi()
        {
            if (_this->data == 0 || _this->size == 0)
            {
                return null;
            }

            return Marshal.PtrToStringAnsi(_this->data, (int)_this->size);
        }
    }
}
