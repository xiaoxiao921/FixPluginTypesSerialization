using System;
using FixPluginTypesSerialization.UnityPlayer.Structs.Default;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v5.v0
{
    [ApplicableToUnityVersionsSince("3.4.0")]
    public class IsFileCreatedParam : IIsFileCreatedParam
    {
        public IsFileCreatedParam()
        {

        }

        public IsFileCreatedParam(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer { get; set; }

        public unsafe string ToStringAnsi()
        {
            return UseRightStructs.GetStruct<IAbsolutePathString>(Pointer).ToStringAnsi();
        }
    }
}