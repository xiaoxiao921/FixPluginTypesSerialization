using BepInEx;
using BepInEx.Configuration;
using FixPluginTypesSerialization.Patchers;
using FixPluginTypesSerialization.UnityPlayer.Structs.v2017.v1;
using FixPluginTypesSerialization.Util;
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
            _config.Bind("Overrides", nameof(UnityVersionOverride),
                "",
                "Unity version is Major.Minor.Patch format i.e. 2017.2.1." + Environment.NewLine +
                "If specified, this version will be used instead of auto-detection" + Environment.NewLine +
                "from executable info. Specify only if the patcher doesn't work otherwise.");

        internal static ConfigEntry<FunctionOffsetLookup> FunctionOffsetLookupType =
            _config.Bind("Overrides", nameof(FunctionOffsetLookupType),
                FunctionOffsetLookup.PreferSupportedVersions,
                $"{nameof(FunctionOffsetLookup.PreferSupportedVersions)} - using values for supported versions, " +
                "if a version is not supported trying to use pdb." + Environment.NewLine +
                $"{nameof(FunctionOffsetLookup.PreferPdb)} - using pdb, it will be downloaded from  Unity symbols server," +
                "if there is no pdb or download failed trying to use values for supported versions." + Environment.NewLine +
                $"{nameof(FunctionOffsetLookup.Manual)} - using offsets from [Cache] section of the config, " +
                $"which you need to specify yourself as hex values.");

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

        internal static ConfigEntry<string> ConvertSeparatorsToPlatformOffset =
            _config.Bind("Cache", nameof(ConvertSeparatorsToPlatformOffset),
                "00",
                $"The in-memory offset of the " +
                $"{nameof(ConvertSeparatorsToPlatform)} function.");

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
