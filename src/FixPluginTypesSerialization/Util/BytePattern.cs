using System;
using System.Linq;

namespace FixPluginTypesSerialization.Util
{
    internal class BytePattern
    {
        private readonly byte?[] pattern;
        private int[] jumpTable;

        public BytePattern(string bytes)
        {
            pattern = bytes.ParseHexBytes();
            CreateJumpTable();
        }

        public BytePattern(byte[] bytes)
        {
            pattern = bytes.Cast<byte?>().ToArray();
            CreateJumpTable();
        }

        public int Length => pattern.Length;

        public bool IsE8 => pattern[0] == 0xE8;

        public static implicit operator BytePattern(string pattern)
        {
            return new BytePattern(pattern);
        }

        public static implicit operator BytePattern(byte[] pattern)
        {
            return new BytePattern(pattern);
        }

        // Table-building algorithm from KMP
        private void CreateJumpTable()
        {
            jumpTable = new int[pattern.Length];

            var substrCandidate = 0;
            jumpTable[0] = -1;
            for (var i = 1; i < pattern.Length; i++, substrCandidate++)
                if (pattern[i] == pattern[substrCandidate])
                {
                    jumpTable[i] = jumpTable[substrCandidate];
                }
                else
                {
                    jumpTable[i] = substrCandidate;
                    while (substrCandidate >= 0 && pattern[i] != pattern[substrCandidate])
                        substrCandidate = jumpTable[substrCandidate];
                }
        }

        public unsafe long Match(IntPtr start, long maxSize)
        {
            var ptr = (byte*) start.ToPointer();
            for (long j = 0, k = 0; j < maxSize;)
                if (pattern[k] == null || ptr[j] == pattern[k])
                {
                    j++;
                    k++;
                    if (k == pattern.Length)
                        return j - k;
                }
                else
                {
                    k = jumpTable[k];
                    if (k >= 0) continue;
                    j++;
                    k++;
                }

            return 0;
        }
    }
}