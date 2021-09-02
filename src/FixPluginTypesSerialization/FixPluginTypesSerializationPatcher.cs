using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FixPluginTypesSerialization.Patchers;
using FixPluginTypesSerialization.Util;
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
                Log.Error($"Failed to initialize plugin types serialization fix: ({e.GetType()}) {e.Message}. Some plugins may not work properly.");
                Log.Error(e);
            }
        }

        private static void InitializeInternal()
        {
            DetourUnityPlayer();
        }

        private static unsafe void DetourUnityPlayer()
        {
            var unityDllPath = Path.Combine(BepInEx.Paths.GameRootPath, "UnityPlayer.dll");

            var pdbReader = new MiniPdbReader(unityDllPath);

            static bool IsUnityPlayer(ProcessModule p)
            {
                return p.ModuleName.ToLowerInvariant().Contains("unityplayer");
            }

            var proc = Process.GetCurrentProcess().Modules
                .Cast<ProcessModule>()
                .FirstOrDefault(IsUnityPlayer) ?? Process.GetCurrentProcess().MainModule;

            var awakeFromLoadPatcher = new AwakeFromLoad();
            var isAssemblyCreatedPatcher = new IsAssemblyCreated();
            var readStringFromFilePatcher = new ReadStringFromFile();

            awakeFromLoadPatcher.Patch(proc.BaseAddress, proc.ModuleMemorySize, pdbReader, Config.MonoManagerAwakeFromLoadOffset);
            isAssemblyCreatedPatcher.Patch(proc.BaseAddress, proc.ModuleMemorySize, pdbReader, Config.MonoManagerIsAssemblyCreatedOffset);
            readStringFromFilePatcher.Patch(proc.BaseAddress, proc.ModuleMemorySize, pdbReader, Config.ReadStringFromFileOffset);
        }
    }
}