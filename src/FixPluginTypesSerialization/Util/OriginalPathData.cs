using System;
using System.Collections.Generic;
using System.Text;

namespace FixPluginTypesSerialization.Util
{
    public struct OriginalPathData
    {
        public IntPtr thisRef;
        public IntPtr thisDataRef;
        public ulong size;

        public OriginalPathData(IntPtr thisRef, IntPtr thisDataRef, ulong size)
        {
            this.thisRef = thisRef;
            this.thisDataRef = thisDataRef;
            this.size = size;
        }
    }
}
