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

        public void Patch(IntPtr unityModule, int moduleSize, MiniPdbReader pdbReader, ConfigEntry<string> functionOffsetCache)
        {
            var offset = PatternDiscover.Discover(unityModule, moduleSize, pdbReader, functionOffsetCache, PdbPatterns, SigPatterns);
            if (offset != IntPtr.Zero)
            {
                Apply(offset);
            }
        }

        protected abstract unsafe void Apply(IntPtr from);
    }
}
