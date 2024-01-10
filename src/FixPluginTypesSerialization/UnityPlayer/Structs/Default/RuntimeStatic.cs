using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.Default
{
    public unsafe struct RuntimeStatic<T> where T : struct
    {
#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
        public T* value;
#pragma warning restore CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
        public int label;
        public int unknown1;
        public ulong memAlign;
        public fixed byte memAreaName[32];
        public fixed byte memObjectName[32];
        public RegisterRuntimeInitializeAndCleanup registerCallbacks;
        public ulong unknown2;
        public ulong unknown3;
        public ulong unknown4;
        public ulong unknown5;
        public ulong unknown6;
        public ulong unknown7;
        public ReadWriteSpinLock @lock;
    }

    public unsafe struct RegisterRuntimeInitializeAndCleanup
    {
        public int order;
        public IntPtr userData;
        public IntPtr init;
        public IntPtr cleanup;
        public bool initCalled;
        public RegisterRuntimeInitializeAndCleanup* m_Next;
        public RegisterRuntimeInitializeAndCleanup* m_Prev;
    }

    public struct ReadWriteSpinLock
    {
        public long m_Counter;
        public ulong unknown1;
        public ulong unknown2;
        public ulong unknown3;
        public ulong unknown4;
        public ulong unknown5;
        public ulong unknown6;
        public ulong unknown7;
    };
}
