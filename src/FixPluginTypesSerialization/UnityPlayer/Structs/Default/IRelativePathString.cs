using System;
using System.Collections;
using System.Collections.Generic;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.Default
{
    public interface IRelativePathString : INativeStruct
    {
        public unsafe void FixAbsolutePath();

        public unsafe string ToStringAnsi();
    }
}
