using FixPluginTypesSerialization.Patchers;
using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v2017.v1
{
    [ApplicableToUnityVersionsSince("2017.1.0")]
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

        public unsafe bool CreatePluginAbsolutePath(out StringStorageDefaultV1 output)
        {
            var fixedOutput = new StringStorageDefaultV1();

            var result = CreatePluginAbsolutePath((IntPtr)(&fixedOutput));
            output = fixedOutput;
            return result;
        }

        public unsafe bool CreatePluginAbsolutePath(IntPtr output)
        {
            if (_this->size == 0)
            {
                return false;
            }

            var data = _this->data;
            if (data == 0)
            {
                data = (IntPtr)(Pointer.ToInt64() + 8);
            }

            var pathNameStr = Marshal.PtrToStringAnsi(data, (int)_this->size);

            var fileNameStr = Path.GetFileName(pathNameStr);
            var newPath = FixPluginTypesSerializationPatcher.PluginPaths.FirstOrDefault(p => Path.GetFileName(p) == fileNameStr);
            if (string.IsNullOrEmpty(newPath))
            {
                return false;
            }

            var newNativePath = CommonUnityFunctions.MallocString(newPath, UseRightStructs.LabelMemStringId, out var length);

            var str = (StringStorageDefaultV1*)output;
            str->data = newNativePath;
            str->capacity = length - 1;
            str->extra = 0;
            str->size = length - 1;
            str->label = UseRightStructs.LabelMemStringId;

            return true;
        }
        
        public unsafe void AppendToScriptingAssemblies(IntPtr json, IEnumerable<string> pluginNames)
        {
            throw new NotSupportedException("ScriptingAssemblies start from Unity 2020.2");
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
