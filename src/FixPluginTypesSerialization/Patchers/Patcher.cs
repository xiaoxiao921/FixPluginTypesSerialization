using System;
using BepInEx.Configuration;
using FixPluginTypesSerialization.Util;

namespace FixPluginTypesSerialization.Patchers
{
    internal abstract class Patcher
    {
        protected abstract BytePattern[] PdbPatterns { get; }

        public unsafe void Patch(PatternDiscoverer patternDiscoverer, ConfigEntry<string> functionOffsetCache)
        {
            var offset = patternDiscoverer.Discover(functionOffsetCache, PdbPatterns);
            if (offset != IntPtr.Zero)
            {
                Apply(offset);
            }
        }

        protected abstract unsafe void Apply(IntPtr from);
    }
}
