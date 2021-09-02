using System.Collections.Generic;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.Default
{
    public interface IMonoManager : INativeStruct
    {
        public int AssemblyCount { get; }

        public unsafe void CopyNativeAssemblyListToManaged();

        public void AddAssembliesToManagedList(List<string> pluginAssemblyPaths);

        public unsafe void AllocNativeAssemblyListFromManaged();

        public unsafe void PrintAssemblies();

        public unsafe void RestoreOriginalAssemblyList();
    }
}
