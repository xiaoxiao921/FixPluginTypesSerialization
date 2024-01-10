using BepInEx;
using BepInEx.Configuration;
using FixPluginTypesSerialization.Patchers;
using FixPluginTypesSerialization.UnityPlayer.Structs.v2017.v1;
using System;
using System.IO;

namespace FixPluginTypesSerialization
{
    internal static class Config
    {
        private static readonly ConfigFile _config =
            new(Path.Combine(Paths.ConfigPath, nameof(FixPluginTypesSerialization) + ".cfg"),
                true);

        internal static ConfigEntry<string> UnityVersionOverride =
            _config.Bind("Cache", nameof(UnityVersionOverride),
                "",
                "Unity version is Major.Minor.Patch format i.e. 2017.2.1. " +
                "If specified, this version will be used instead of auto-detection " +
                "from executable info. Specify only if the patcher doesn't work otherwise.");

        internal static ConfigEntry<string> LastDownloadedGUID =
            _config.Bind("Cache", nameof(LastDownloadedGUID),
                "000000000000000000000000000000000",
                "The GUID of the last downloaded UnityPlayer pdb file." + Environment.NewLine +
                "If this GUID matches with the current one," + Environment.NewLine +
                "the offsets for the functions below will be used" + Environment.NewLine +
                "instead of generating them at runtime.");

        internal static ConfigEntry<string> MonoManagerAwakeFromLoadOffset =
            _config.Bind("Cache", nameof(MonoManagerAwakeFromLoadOffset),
                "00",
                "The in-memory offset of the " +
                $"{nameof(MonoManager) + "::" + nameof(AwakeFromLoad)} function.");

        internal static ConfigEntry<string> MonoManagerIsAssemblyCreatedOffset =
            _config.Bind("Cache", nameof(MonoManagerIsAssemblyCreatedOffset),
                "00",
                $"The in-memory offset of the " +
                $"{nameof(MonoManager) + "::" + nameof(IsAssemblyCreated)} function.");

        internal static ConfigEntry<string> IsFileCreatedOffset =
            _config.Bind("Cache", nameof(IsFileCreatedOffset),
                "00",
                $"The in-memory offset of the " +
                $"{nameof(IsFileCreated)} function.");

        internal static ConfigEntry<string> ScriptingManagerDeconstructorOffset =
            _config.Bind("Cache", nameof(ScriptingManagerDeconstructorOffset),
                "00",
                $"The in-memory offset of the " +
                $"{nameof(ScriptingManagerDeconstructor)} function.");

        internal static ConfigEntry<string> PathToAbsolutePathOffset =
            _config.Bind("Cache", nameof(PathToAbsolutePath),
                "00",
                $"The in-memory offset of the " +
                $"{nameof(PathToAbsolutePath)} function.");

        internal static ConfigEntry<string> FreeAllocInternalOffset =
            _config.Bind("Cache", nameof(FreeAllocInternalOffset),
                "00",
                $"The in-memory offset of the " +
                $"free_alloc_internal function.");

        internal static ConfigEntry<string> MallocInternalOffset =
            _config.Bind("Cache", nameof(MallocInternalOffset),
                "00",
                $"The in-memory offset of the " +
                $"malloc_internal function.");

        internal static ConfigEntry<string> ScriptingAssembliesOffset =
            _config.Bind("Cache", nameof(ScriptingAssembliesOffset),
                "00",
                $"The in-memory offset of the " +
                $"m_ScriptingAssemblies global field.");
    }
}
