using FixPluginTypesSerialization.Patchers;
using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using FixPluginTypesSerialization.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FixPluginTypesSerialization.UnityPlayer.Structs.v2021.v1
{
    [ApplicableToUnityVersionsSince("2021.1.0")]
    public class RelativePathString : IRelativePathString
    {
        public RelativePathString()
        {

        }

        public RelativePathString(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer { get; set; }

        private unsafe BasicStringRef* _this => (BasicStringRef*)Pointer;

        public unsafe bool CreatePluginAbsolutePath(out StringStorageDefaultV1 output)
        {
            var fixedOutput = new StringStorageDefaultV1();

            var result = CreatePluginAbsolutePath((IntPtr)(&fixedOutput));
            output = fixedOutput;
            return result;
        }

        public unsafe bool CreatePluginAbsolutePath(IntPtr output)
        {
            if (_this->data == 0 || _this->size == 0)
            {
                return false;
            }

            var pathNameStr = Marshal.PtrToStringAnsi(_this->data, (int)_this->size);
            var newPath = FixPluginTypesSerializationPatcher.PluginPaths.FirstOrDefault(p => Path.GetFileName(p) == Path.GetFileName(pathNameStr));

            if (string.IsNullOrEmpty(newPath))
            {
                return false;
            }
            
            var newNativePath = CommonUnityFunctions.MallocString(newPath, UseRightStructs.LabelMemStringId, out var length);
            var str = (StringStorageDefaultV2*)output;
            str->union = new StringStorageDefaultV2Union
            {
                heap = new HeapAllocatedRepresentation
                {
                    data = newNativePath,
                    capacity = length,
                    size = length
                }
            };
            str->data_repr = StringRepresentation.Heap;
            str->label = UseRightStructs.LabelMemStringId;

            return true;
        }

        public unsafe void AppendToScriptingAssemblies(IntPtr json, IEnumerable<string> pluginNames)
        {
            var path = ToStringAnsi();
            if (!path.EndsWith("ScriptingAssemblies.json", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var jsonPtr = (StringStorageDefaultV2*)json;

            var jsonStr = jsonPtr->data_repr switch
            {
                StringRepresentation.Embedded => Marshal.PtrToStringAnsi((nint)jsonPtr),
                _ => Marshal.PtrToStringAnsi(jsonPtr->union.heap.data, (int)jsonPtr->union.heap.size)
            };

            var closingBracketIndex = jsonStr.IndexOf(']');
            IsAssemblyCreated.VanillaAssemblyCount = jsonStr.Take(closingBracketIndex).Count(c => c == ',') + 1;

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(jsonStr, 0, closingBracketIndex);
            foreach (var pluginName in pluginNames)
            {
                stringBuilder.Append(",\"").Append(pluginName).Append('"');
            }
            stringBuilder.Append(jsonStr, closingBracketIndex, jsonStr.Length - closingBracketIndex);
            
            var newNativeJson = CommonUnityFunctions.MallocString(stringBuilder.ToString(), jsonPtr->label, out var length);

            if (jsonPtr->data_repr != StringRepresentation.Embedded)
            {
                CommonUnityFunctions.FreeAllocInternal(jsonPtr->union.heap.data, jsonPtr->label);
            }

            jsonPtr->union = new StringStorageDefaultV2Union
            {
                heap = new HeapAllocatedRepresentation
                {
                    data = newNativeJson,
                    size = length - 1,
                    capacity = length - 1
                }
            };
            jsonPtr->data_repr = StringRepresentation.Heap;
        }

        public unsafe string ToStringAnsi()
        {
            if (_this->data == 0 || _this->size == 0)
            {
                return null;
            }

            return Marshal.PtrToStringAnsi(_this->data, (int)_this->size);
        }
    }
}
