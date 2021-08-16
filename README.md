# FixPluginTypesSerialization

Hook into the native Unity engine for adding BepInEx plugin assemblies into the assembly list that is normally used for the assemblies sitting in the game Managed/ folder.

This solve a bug where custom Serializable structs and such stored in plugin assemblies are not properly getting deserialized by the engine.

### Installation

- Copy the `FixPluginTypesSerialization.dll` into your `BepInEx/patchers` folder.

### Special Thanks

- Horse and Marco. [For the original code](https://github.com/BepInEx/BepInEx.Debug/tree/master/src/MirrorInternalLogs)