using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace FixPluginTypesSerialization.Util
{
    // All credits goes to https://github.com/0x0ade/MidiToMGBA/blob/master/src/DynamicDll.cs

    internal static class NativeLibraryHelper
    {
        private readonly static IntPtr NULL = IntPtr.Zero;

        public static Dictionary<string, string> DllMap = new();

        [DllImport("kernel32")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32")]
        private static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        private static IntPtr _EntryPoint = NULL;

        public static IntPtr EntryPoint
        {
            get
            {
                if (_EntryPoint != NULL)
                    return _EntryPoint;

                return _EntryPoint = OpenLibrary(null);
            }
        }

        public static IntPtr OpenLibrary(string name)
        {
            if (DllMap.TryGetValue(name, out string mapped))
                name = mapped;

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                IntPtr lib = GetModuleHandle(name);
                if (lib == NULL)
                {
                    lib = LoadLibrary(name);
                }

                return lib;
            }

            return NULL;
        }

        public static IntPtr GetFunction(this IntPtr lib, string name)
        {
            if (lib == NULL)
                return NULL;

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                return GetProcAddress(lib, name);

            return NULL;
        }

        public static T GetDelegate<T>(this IntPtr lib, string name) where T : class
        {
            if (lib == NULL)
                return null;

            IntPtr s = lib.GetFunction(name);
            if (s == NULL)
                return null;

            return s.AsDelegate<T>();
        }

        public static T GetDelegateAtRVA<T>(this IntPtr basea, long rva) where T : class
        {
            return new IntPtr(basea.ToInt64() + rva).AsDelegate<T>();
        }

        public static T AsDelegate<T>(this IntPtr s) where T : class
        {
            return Marshal.GetDelegateForFunctionPointer(s, typeof(T)) as T;
        }

        [DllImport("kernel32")]
        private static extern uint GetCurrentThreadId();

        public static ulong CurrentThreadId
        {
            get
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    return GetCurrentThreadId();

                return 0;
            }
        }

        public static void ResolveDynamicDllImports(this Type type)
        {
            foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic |
                                                       BindingFlags.Static))
            {
                bool found = true;
                foreach (DynamicDllImportAttribute attrib in field.GetCustomAttributes(
                    typeof(DynamicDllImportAttribute), true))
                {
                    found = false;
                    IntPtr asm = OpenLibrary(attrib.DLL);
                    if (asm == NULL)
                        continue;

                    foreach (string ep in attrib.EntryPoints)
                    {
                        IntPtr func = asm.GetFunction(ep);
                        if (func == NULL)
                            continue;
                        field.SetValue(null, Marshal.GetDelegateForFunctionPointer(func, field.FieldType));
                        found = true;
                        break;
                    }

                    if (found)
                        break;
                }

                if (!found)
                    throw new EntryPointNotFoundException(
                        $"No matching entry point found for {field.Name} in {field.DeclaringType.FullName}");
            }
        }

        [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
        public class DynamicDllImportAttribute : Attribute
        {
            public string DLL;
            public string[] EntryPoints;

            public DynamicDllImportAttribute(string dll, params string[] entryPoints)
            {
                DLL = dll;
                EntryPoints = entryPoints;
            }
        }
    }
}
