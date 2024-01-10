using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.Default
{
    //2021
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct StringStorageDefaultV2
    {
        public StringStorageDefaultV2Union union;
        public StringRepresentation data_repr;
        public int label;

        public unsafe bool IsValid()
        {
            switch (data_repr)
            {
                case StringRepresentation.Heap:
                    return union.heap.data > 0;
                default:
                    return true;
            };
        }
    }

    [StructLayout(LayoutKind.Explicit, Pack = 8)]
    public struct StringStorageDefaultV2Union
    {
        [FieldOffset(0)]
        public StackAllocatedRepresentation embedded;
        [FieldOffset(0)]
        public HeapAllocatedRepresentation heap;
    }

    public struct StackAllocatedRepresentation
    {
        public unsafe fixed byte data[25];
    }

    public struct HeapAllocatedRepresentation
    {
        public nint data;
        public ulong capacity;
        public ulong size;
    }

    public enum StringRepresentation : int
    {
        Heap,
        Embedded,
        External
    }
}
