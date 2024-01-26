using System;
using System.Collections.Generic;
using System.Globalization;
using BepInEx.Configuration;
using FixPluginTypesSerialization.UnityPlayer;

namespace FixPluginTypesSerialization.Util
{
    internal class PatternDiscoverer
    {
        private readonly IntPtr unityModule;
        private readonly MiniPdbReader pdbReader;
        private readonly bool usePdb;
        private readonly Dictionary<string, long> functionOffsets;

        public PatternDiscoverer(IntPtr unityModule, string unityPlayerPath)
        {
            this.unityModule = unityModule;

            switch (Config.FunctionOffsetLookupType.Value)
            {
                case FunctionOffsetLookup.Manual:
                    return;
                case FunctionOffsetLookup.PreferPdb:
                    pdbReader = new MiniPdbReader(unityPlayerPath);
                    usePdb = pdbReader.IsPdbAvailable;
                    if (!usePdb)
                    {
                        if (!FunctionOffsets.TryGet(UseRightStructs.UnityVersion, out functionOffsets))
                        {
                            throw new NotSupportedException($"Pdb not found and {UseRightStructs.UnityVersion} is not a supported version");
                        }
                        else
                        {
                            Log.Info("Found offsets for current version, using them.");
                        }
                    }
                    break;
                case FunctionOffsetLookup.PreferSupportedVersions:
                    if (!FunctionOffsets.TryGet(UseRightStructs.UnityVersion, out functionOffsets))
                    {
                        pdbReader = new MiniPdbReader(unityPlayerPath);
                        usePdb = pdbReader.IsPdbAvailable;
                        if (!usePdb)
                        {
                            throw new NotSupportedException($"{UseRightStructs.UnityVersion} is not a supported version and pdb not found");
                        }
                    }
                    else
                    {
                        Log.Info("Found offsets for current version, using them.");
                    }
                    break;
                default:
                    throw new ArgumentException(nameof(Config.FunctionOffsetLookupType));
            }
        }

        public unsafe IntPtr Discover(ConfigEntry<string> functionOffsetCache, BytePattern[] pdbPatterns)
        {
            long offset;
            if (usePdb)
            {
                return DiscoverWithPdb(functionOffsetCache, pdbPatterns);
            }

            if (functionOffsets != null)
            {
                offset = functionOffsets[functionOffsetCache.Definition.Key];
                functionOffsetCache.Value = offset.ToString("X2");

                if (offset == 0)
                {
                    return IntPtr.Zero;
                }

                return (IntPtr)(unityModule.ToInt64() + offset);
            }

            if (long.TryParse(functionOffsetCache.Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out offset))
            {
                return (IntPtr)(unityModule.ToInt64() + offset);
            }

            return IntPtr.Zero;
        }

        private IntPtr DiscoverWithPdb(ConfigEntry<string> functionOffsetCache, BytePattern[] pdbPatterns)
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
                functionOffsetCache.Value = functionOffset.ToString("X2");
            }

            return (IntPtr)(unityModule.ToInt64() + functionOffset.ToInt64());
        }
    }
}
