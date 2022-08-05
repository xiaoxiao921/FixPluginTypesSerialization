using BepInEx;
using BepInEx.Configuration;
using FixPluginTypesSerialization.Patchers;
using FixPluginTypesSerialization.UnityPlayer.Structs.v2018;
using System;
using System.IO;

namespace FixPluginTypesSerialization
{
    internal static class Config
    {
        private static readonly ConfigFile _config =
            new(Path.Combine(Paths.ConfigPath, nameof(FixPluginTypesSerialization) + ".cfg"),
                true);

        internal static ConfigEntry<string> LastDownloadedGUID =
            _config.Bind("Cache", nameof(LastDownloadedGUID),
                "B8A8A8D3234C456C9B5E4D89FD56632C1",
                "The GUID of the last downloaded UnityPlayer pdb file." + Environment.NewLine +
                "If this GUID matches with the current one," + Environment.NewLine +
                "the offsets for the functions below will be used" + Environment.NewLine +
                "instead of generating them at runtime.");

        internal static ConfigEntry<string> MonoManagerAwakeFromLoadOffset =
            _config.Bind("Cache", nameof(MonoManagerAwakeFromLoadOffset),
                "8EA3A0",
                "The in-memory offset of the " +
                $"{nameof(MonoManager) + "::" + nameof(AwakeFromLoad)} function.");

        internal static ConfigEntry<string> MonoManagerIsAssemblyCreatedOffset =
            _config.Bind("Cache", nameof(MonoManagerIsAssemblyCreatedOffset),
                "8ECFB0",
                $"The in-memory offset of the " +
                $"{nameof(MonoManager) + "::" + nameof(IsAssemblyCreated)} function.");

        internal static ConfigEntry<string> ReadStringFromFileOffset =
            _config.Bind("Cache", nameof(ReadStringFromFileOffset),
                "879A40",
                $"The in-memory offset of the " +
                $"{nameof(ReadStringFromFile)} function.");

        internal static ConfigEntry<string> ScriptingManagerDeconstructorOffset =
            _config.Bind("Cache", nameof(ScriptingManagerDeconstructorOffset),
                "8C0B50",
                $"The in-memory offset of the " +
                $"{nameof(ScriptingManagerDeconstructor)} function.");
    }
}
