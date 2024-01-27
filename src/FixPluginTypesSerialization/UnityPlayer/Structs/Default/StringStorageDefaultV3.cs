using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.Default
{
    //2023
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct StringStorageDefaultV3
    {
        public StringStorageDefaultV3Union union;
        public int label;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 8)]
    public struct StringStorageDefaultV3Union
    {
        [FieldOffset(0)]
        public StackAllocatedRepresentationV3 embedded;
        [FieldOffset(0)]
        public HeapAllocatedRepresentationV3 heap;
    }

    public struct StackAllocatedRepresentationV3
    {
        public unsafe fixed byte data[31];
        public StringStorageDefaultV3Flags flags;
    }

    public struct StringStorageDefaultV3Flags
    {
        public byte flags;

        public bool IsHeap
        {
            get => (flags & (1 << 6)) > 0;
            set
            {
                if (value)
                {
                    flags = 0x5f;
                }
                else
                {
                    flags = 0;
                }
            }
        }
        public bool IsExternal
        {
            get => (flags & (1 << 7)) > 0;
            set
            {
                if (value)
                {
                    flags = 0xff;
                }
                else
                {
                    flags = 0;
                }
            }
        }

        public bool IsEmbedded
        {
            get => flags < 0x40;
            set => IsHeap = !value;
        }

        public static implicit operator int(StringStorageDefaultV3Flags f) => f.flags;
        public static implicit operator byte(StringStorageDefaultV3Flags f) => f.flags;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct HeapAllocatedRepresentationV3
    {
        [FieldOffset(0)]
        public nint data;
        [FieldOffset(0x8)]
        public ulong capacity;
        [FieldOffset(0x10)]
        public ulong size;
        [FieldOffset(0x1f)]
        public StringStorageDefaultV3Flags flags;
    }
}
