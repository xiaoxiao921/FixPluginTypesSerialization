using System;
using System.Linq;
using FixPluginTypesSerialization.Util;

namespace FixPluginTypesSerialization.Patchers
{
    internal abstract class Patcher
    {
        protected abstract BytePattern[] Patterns { get; }

        public void Patch(IntPtr unityModule, int moduleSize)
        {
            var match = FindMatch(unityModule, moduleSize);
            if (match == IntPtr.Zero)
                return;

            Apply(match);
        }

        private unsafe IntPtr FindMatch(IntPtr start, int maxSize)
        {
            var match = Patterns.Select(p => new { p, res = p.Match(start, maxSize) })
                .FirstOrDefault(m => m.res >= 0);
            if (match == null)
            {
                Log.LogError("No match found, cannot hook ! Please report it to the r2api devs!");
                return IntPtr.Zero;
            }

            var ptr = (byte*)start.ToPointer();
            Log.LogDebug($"Found at {match.res:X} ({start.ToInt64() + match.res:X})");

            var addr = start.ToInt64() + match.res;

            // https://stackoverflow.com/a/10376930
            if (match.p.IsE8)
            {
                int e8_offset = *(int*)(start.ToInt64() + match.res + 1);
                Log.LogDebug($"Parsed e8_offset: {e8_offset:X}");
                addr = addr + 5 + e8_offset;
            }

            Log.LogDebug($"memory address: {addr:X} (image base: {start.ToInt64():X})");

            return new IntPtr(addr);
        }

        protected abstract unsafe void Apply(IntPtr from);
    }
}