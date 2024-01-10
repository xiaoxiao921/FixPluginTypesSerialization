using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using FixPluginTypesSerialization.UnityPlayer;
using UnityEngine;

namespace FixPluginTypesSerialization.Util
{
    internal class CommonUnityFunctions
    {
        public enum AllocateOptions : int
        {
            None = 0,
            NullIfOutOfMemory
        };

        private delegate IntPtr MallocInternalFunc(ulong size, ulong allign, int label, AllocateOptions allocateOptions, IntPtr file, int line);
        private static MallocInternalFunc mallocInternal;

        private delegate void FreeAllocInternalV1Func(IntPtr ptr, int label);
        private delegate void FreeAllocInternalV2Func(IntPtr ptr, int label, IntPtr file, int line);
        private static FreeAllocInternalV1Func freeAllocInternalV1;
        private static FreeAllocInternalV2Func freeAllocInternalV2;

        public static IntPtr ScriptingAssemblies { get; private set; }

        public static void Init(IntPtr unityModule, int moduleSize, MiniPdbReader pdbReader)
        {
            var mallocInternalAddress = PatternDiscover.Discover(
                unityModule,
                moduleSize,
                pdbReader,
                Config.MallocInternalOffset,
                [Encoding.ASCII.GetBytes("malloc_internal")],
                []);
            if (mallocInternalAddress != IntPtr.Zero)
            {
                mallocInternal = (MallocInternalFunc)Marshal.GetDelegateForFunctionPointer(mallocInternalAddress, typeof(MallocInternalFunc));
            }

            var freeAllocInternalAddress = PatternDiscover.Discover(
                unityModule,
                moduleSize,
                pdbReader,
                Config.FreeAllocInternalOffset,
                [Encoding.ASCII.GetBytes("free_alloc_internal")],
                []);
            if (freeAllocInternalAddress != IntPtr.Zero)
            {
                if (UseRightStructs.UnityVersion >= new Version(2019, 3))
                {
                    freeAllocInternalV2 = (FreeAllocInternalV2Func)Marshal.GetDelegateForFunctionPointer(freeAllocInternalAddress, typeof(FreeAllocInternalV2Func));
                }
                else
                {
                    freeAllocInternalV1 = (FreeAllocInternalV1Func)Marshal.GetDelegateForFunctionPointer(freeAllocInternalAddress, typeof(FreeAllocInternalV1Func));
                }
            }

            var scriptingAssembliesAddress = PatternDiscover.Discover(
                unityModule,
                moduleSize,
                pdbReader,
                Config.ScriptingAssembliesOffset,
                [Encoding.ASCII.GetBytes("m_ScriptingAssemblies@")],
                []);
            if (scriptingAssembliesAddress != IntPtr.Zero)
            {
                ScriptingAssemblies = scriptingAssembliesAddress;
            }
        }

        public unsafe static IntPtr MallocString(string str, int label, out ulong length)
        {
            //I couldn't for the life of me find how to adequately convert c# string to ANSI and fill existing pointer
            //that we would get from mallocInternal from c# so we're doing it this way
            var strPtr = Marshal.StringToHGlobalAnsi(str);

            length = 0ul;
            for (var c = (byte*)strPtr; *c != 0; c++, length++) { }

            var allocPtr = mallocInternal(length + 1, 0x10, label, AllocateOptions.NullIfOutOfMemory, IntPtr.Zero, 0);

            for (var i = 0ul; i <= length; i++)
            {
                ((byte*)allocPtr)[i] = ((byte*)strPtr)[i];
            }

            Marshal.FreeHGlobal(strPtr);

            return allocPtr;
        }

        public static IntPtr MallocInternal(ulong size, ulong allign, int label)
        {
            return mallocInternal(size, allign, label, AllocateOptions.NullIfOutOfMemory, IntPtr.Zero, 0);
        }

        public static void FreeAllocInternal(IntPtr ptr, int label)
        {
            if (UseRightStructs.UnityVersion >= new Version(2019, 3))
            {
                freeAllocInternalV2(ptr, label, IntPtr.Zero, 0);
            }
            else
            {
                freeAllocInternalV1(ptr, label);
            }
        }
    }
}
