using System;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.Default
{
    public interface IAssemblyString : INativeStruct
    {
        public unsafe void FixAbsolutePath();

        /// <summary>
        /// _this is a const char*.
        /// </summary>
        /// <param name="constCharPtr"></param>
        public unsafe void RestoreOriginalString(IntPtr constCharPtr);

        public unsafe string ToStringAnsi();
    }
}
