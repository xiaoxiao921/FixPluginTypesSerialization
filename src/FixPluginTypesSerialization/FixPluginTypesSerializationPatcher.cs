using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FixPluginTypesSerialization.Patchers;
using Mono.Cecil;

namespace FixPluginTypesSerialization
{
    internal static class FixPluginTypesSerializationPatcher
    {
        public static IEnumerable<string> TargetDLLs { get; } = new string[0];

        public static List<string> PluginPaths =
            Directory.GetFiles(BepInEx.Paths.PluginPath, "*.dll", SearchOption.AllDirectories).ToList();

        public static void Patch(AssemblyDefinition ass)
        {
        }

        public static void Initialize()
        {
            Log.Init();

            try
            {
                InitializeInternal();
            }
            catch (Exception e)
            {
                Log.LogError($"Failed to initialize plugin types serialization fix: ({e.GetType()}) {e.Message}. Some plugins may not work properly.");
                Log.LogError(e);
            }
        }

        private static void InitializeInternal()
        {
            DetourUnityPlayer();
        }

        private static void DetourUnityPlayer()
        {
            static bool IsUnityPlayer(ProcessModule p)
            {
                return p.ModuleName.ToLowerInvariant().Contains("unityplayer");
            }

            var proc = Process.GetCurrentProcess().Modules
                .Cast<ProcessModule>()
                .FirstOrDefault(IsUnityPlayer) ?? Process.GetCurrentProcess().MainModule;

            var addAssembliesPatcher = new AddAssembliesPatcher();
            var isAssemblyCreatedPatcher = new IsAssemblyCreatedPatcher();
            var readStringFromFile = new ReadStringFromFile();

            addAssembliesPatcher.Patch(proc.BaseAddress, proc.ModuleMemorySize);
            isAssemblyCreatedPatcher.Patch(proc.BaseAddress, proc.ModuleMemorySize);
            readStringFromFile.Patch(proc.BaseAddress, proc.ModuleMemorySize);
        }
    }
}