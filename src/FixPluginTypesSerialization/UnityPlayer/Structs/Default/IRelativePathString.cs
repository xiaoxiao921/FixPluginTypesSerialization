using System;
using System.Collections;
using System.Collections.Generic;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.Default
{
    public interface IRelativePathString : INativeStruct
    {
        public unsafe bool CreatePluginAbsolutePath(IntPtr output);
        public unsafe bool CreatePluginAbsolutePath(out StringStorageDefaultV1 output);
        public unsafe void AppendToScriptingAssemblies(IntPtr json, IEnumerable<string> pluginNames);

        public unsafe string ToStringAnsi();
    }
}
