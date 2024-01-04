# FixPluginTypesSerialization

Hook into the native Unity engine for adding BepInEx plugin assemblies into the assembly list that is normally used for the assemblies sitting in the game Managed/ folder.

This solve a bug where custom Serializable structs and such stored in plugin assemblies are not properly getting deserialized by the engine.

### Installation

- Copy the `BepInEx\patchers\FixPluginTypesSerialization` folder into your `BepInEx/patchers` folder.

### Adding your own Unity version support

This project only works with Unity versions 2018.4 and 2019.4, which are the two versions RoR2 was using when I wrote this project.

If you want to add your own version, know that I don't have the free time and the will to investigate on how to help you, all I can provide is the text below.

My best advice is to get ida or ghidra and get the unity pdb from their symbol servers (just google unity symbol server for that), and read the pseudo C code from the decompiler, find the assembly lists they populate from the Manager/ folder, and add the bepinex plugins/ assemblies to those assembly lists.

That's basically all this project does.

Be warned that Unity's implementation changes greatly from version to version, so you may have to completely change the way things are approached for the two versions for which I've written this project.

An alternative path to messing with Unity internals directly is to investigate on how to setup a Virtual File System, other modding communities like Skyrim have done it for mod management purposes.

The idea is to redirect all system calls so that the bepinex plugin folder is as if it were in the Managed one.

### Special Thanks

- Horse [for the original code base](https://github.com/BepInEx/BepInEx.Debug/tree/master/src/MirrorInternalLogs)

- 0x0ade [for the NativeLibraryHelper class](https://github.com/0x0ade/MidiToMGBA/blob/master/src/DynamicDll.cs)

- [knah](https://github.com/knah/Il2CppAssemblyUnhollower/)

- KingEnderBrine

- Twiner

- NebNeb for the icon
