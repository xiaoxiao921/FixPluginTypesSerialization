using System;
using System.Collections;
using System.Collections.Generic;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.Default
{
    public interface IAbsolutePathString : INativeStruct
    {
        public unsafe void FixAbsolutePath();

        public unsafe string ToStringAnsi();
    }
}
