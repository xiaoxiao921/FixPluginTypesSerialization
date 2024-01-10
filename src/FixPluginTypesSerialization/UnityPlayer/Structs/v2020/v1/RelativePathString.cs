using FixPluginTypesSerialization.Patchers;
using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v2020.v1
{
    [ApplicableToUnityVersionsSince("2020.1.0")]
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

        private unsafe char* _this => (char*)Pointer;

        public unsafe bool CreatePluginAbsolutePath(out StringStorageDefaultV1 output)
        {
            var fixedOutput = new StringStorageDefaultV1();

            var result = CreatePluginAbsolutePath((IntPtr)(&fixedOutput));
            output = fixedOutput;
            return result;
        }

        public unsafe bool CreatePluginAbsolutePath(IntPtr output)
        {
            var pathNameStr = Marshal.PtrToStringAnsi(Pointer);
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

        public unsafe void AppendToScriptingAssemblies(IntPtr json, IEnumerable<string> pluginNames)
        {
            throw new NotSupportedException("ScriptingAssemblies start from Unity 2020.2");
        }

        public unsafe string ToStringAnsi()
        {
            return Marshal.PtrToStringAnsi(Pointer);
        }
    }
}
