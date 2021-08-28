namespace FixPluginTypesSerialization.UnityPlayer.Structs.Default
{
    public interface IAssemblyString : INativeStruct
    {
        public unsafe void FixAbsolutePath();

        public unsafe void RestoreOriginalString();
    }
}
