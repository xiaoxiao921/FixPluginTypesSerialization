using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BepInEx.Configuration;

namespace FixPluginTypesSerialization.Util
{
    internal class PatternDiscover
    {
        public static IntPtr Discover(
            IntPtr unityModule,
            int moduleSize,
            MiniPdbReader pdbReader,
            ConfigEntry<string> functionOffsetCache,
            BytePattern[] pdbPatterns,
            BytePattern[] sigPatterns)
        {
            if (pdbReader.IsPdbAvailable)
            {
                return DiscoverWithPdb(unityModule, pdbReader, functionOffsetCache, pdbPatterns);
            }

            return DiscoverWithSig(unityModule, moduleSize, sigPatterns);
        }

        public static IntPtr DiscoverWithPdb(IntPtr unityModule, MiniPdbReader pdbReader, ConfigEntry<string> functionOffsetCache, BytePattern[] pdbPatterns)
        {
            IntPtr functionOffset;

            if (pdbReader.UseCache)
            {
                functionOffset = new IntPtr(Convert.ToInt64(functionOffsetCache.Value, 16));

                if (functionOffset == IntPtr.Zero)
                {
                    return functionOffset;
                }
            }
            else
            {
                functionOffset = pdbReader.FindFunctionOffset(pdbPatterns);
                if (functionOffset == IntPtr.Zero)
                {
                    functionOffsetCache.Value = "00";
                    return functionOffset;
                }
                functionOffsetCache.Value = functionOffset.ToString("X");
            }

            return (IntPtr)(unityModule.ToInt64() + functionOffset.ToInt64());
        }

        public unsafe static IntPtr DiscoverWithSig(IntPtr unityModule, int moduleSize, BytePattern[] sigPatterns)
        {
            var match = sigPatterns.Select(p => new { p, res = p.Match(unityModule, moduleSize) })
                .FirstOrDefault(m => m.res > 0);
            if (match == null)
            {
                return IntPtr.Zero;
            }

            var ptr = (byte*)unityModule.ToPointer();
            Log.Info($"Found at {match.res:X} ({unityModule.ToInt64() + match.res:X})");

            var addr = unityModule.ToInt64() + match.res;

            // https://stackoverflow.com/a/10376930
            if (match.p.IsE8)
            {
                int e8_offset = *(int*)(unityModule.ToInt64() + match.res + 1);
                Log.Info($"Parsed e8_offset: {e8_offset:X}");
                addr = addr + 5 + e8_offset;
            }

            Log.Info($"memory address: {addr:X} (image base: {unityModule.ToInt64():X})");

            return new IntPtr(addr);
        }
    }
}
