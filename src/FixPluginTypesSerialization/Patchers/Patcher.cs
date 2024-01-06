using System;
using System.Linq;
using BepInEx.Configuration;
using FixPluginTypesSerialization.Util;

namespace FixPluginTypesSerialization.Patchers
{
    internal abstract class Patcher
    {
        protected abstract BytePattern[] PdbPatterns { get; }
        protected abstract BytePattern[] SigPatterns { get; }

        public bool IsApplied { get; protected set; }

        public void Patch(IntPtr unityModule, int moduleSize, MiniPdbReader pdbReader, ConfigEntry<string> functionOffsetCache)
        {
            if (pdbReader.IsPdbAvailable)
            {
                PatchWithPdb(unityModule, pdbReader, functionOffsetCache);
            }
            else
            {
                PatchWithSig(unityModule, moduleSize);
            }
        }

        internal void PatchWithPdb(IntPtr unityModule, MiniPdbReader pdbReader, ConfigEntry<string> functionOffsetCache)
        {
            IntPtr functionOffset;

            if (pdbReader.UseCache && functionOffsetCache.Value != "INVALID")
            {
                functionOffset = new IntPtr(Convert.ToInt64(functionOffsetCache.Value, 16));

                if (functionOffset == IntPtr.Zero)
                {
                    return;
                }
            }
            else
            {
                functionOffset = pdbReader.FindFunctionOffset(PdbPatterns);
                if (functionOffset == IntPtr.Zero)
                {
                    functionOffsetCache.Value = "00";
                    return;
                }
                functionOffsetCache.Value = functionOffset.ToString("X");
            }

            functionOffset = (IntPtr)(unityModule.ToInt64() + functionOffset.ToInt64());

            Apply(functionOffset);
        }

        internal void PatchWithSig(IntPtr unityModule, int moduleSize)
        {
            var match = FindMatch(unityModule, moduleSize);
            if (match == IntPtr.Zero)
                return;

            Apply(match);
        }

        private unsafe IntPtr FindMatch(IntPtr start, long maxSize)
        {
            var match = SigPatterns.Select(p => new { p, res = p.Match(start, maxSize) })
                .FirstOrDefault(m => m.res > 0);
            if (match == null)
            {
                return IntPtr.Zero;
            }

            var ptr = (byte*)start.ToPointer();
            Log.Info($"Found at {match.res:X} ({start.ToInt64() + match.res:X})");

            var addr = start.ToInt64() + match.res;

            // https://stackoverflow.com/a/10376930
            if (match.p.IsE8)
            {
                int e8_offset = *(int*)(start.ToInt64() + match.res + 1);
                Log.Info($"Parsed e8_offset: {e8_offset:X}");
                addr = addr + 5 + e8_offset;
            }

            Log.Info($"memory address: {addr:X} (image base: {start.ToInt64():X})");

            return new IntPtr(addr);
        }

        protected abstract unsafe void Apply(IntPtr from);
    }
}
