using FixPluginTypesSerialization.UnityPlayer.Structs.Default;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace FixPluginTypesSerialization.UnityPlayer
{
    // https://github.com/BepInEx/BepInEx/blob/master/BepInEx.IL2CPP/Preloader.cs#L93

    // https://github.com/knah/Il2CppAssemblyUnhollower/blob/master/UnhollowerBaseLib/Runtime/UnityVersionHandler.cs

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    internal class ApplicableToUnityVersionsSinceAttribute : Attribute
    {
        public string StartVersion { get; }

        public ApplicableToUnityVersionsSinceAttribute(string startVersion)
        {
            StartVersion = startVersion;
        }
    }

    public static class UseRightStructs
    {
        private static readonly Type[] InterfacesOfInterest;
        private static readonly Dictionary<Type, List<(Version Version, object Handler)>> VersionedHandlers = new();
        private static readonly Dictionary<Type, object> CurrentHandlers = new();

        private static Version _unityVersion;
        public static Version UnityVersion
        {
            get
            {
                if (_unityVersion == null)
                {
                    InitializeUnityVersion();
                }

                return _unityVersion;
            }
        }

        private static void InitializeUnityVersion()
        {
            if (TryInitializeUnityVersion(Process.GetCurrentProcess().MainModule.FileVersionInfo.FileVersion))
                Log.Debug($"Unity version obtained from main application module.");
            else
                Log.Error($"Running under default Unity version. UnityVersionHandler is not initialized.");
        }

        private static bool TryInitializeUnityVersion(string version)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(version))
                    return false;

                var parts = version.Split('.');
                var major = 0;
                var minor = 0;
                var build = 0;

                // Issue #229 - Don't use Version.Parse("2019.4.16.14703470L&ProductVersion")
                bool success = int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out major);
                if (success && parts.Length > 1)
                    success = int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out minor);
                if (success && parts.Length > 2)
                    success = int.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out build);

                if (!success)
                {
                    Log.Error($"Failed to parse Unity version: {version}");
                    return false;
                }

                _unityVersion = new Version(major, minor, build);
                Log.Info($"Running under Unity v{UnityVersion}");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to parse Unity version: {ex}");
                return false;
            }
        }

        static UseRightStructs()
        {
            var allTypes = GetAllTypesSafe();
            InterfacesOfInterest = allTypes.Where(t => t.IsInterface && typeof(INativeStruct).IsAssignableFrom(t) && t != typeof(INativeStruct)).ToArray();

            foreach (var i in InterfacesOfInterest)
            {
                VersionedHandlers[i] = new();
            }

            foreach (var handlerImpl in allTypes.Where(t => !t.IsAbstract && InterfacesOfInterest.Any(i => i.IsAssignableFrom(t))))
            {
                foreach (var startVersion in handlerImpl.GetCustomAttributes<ApplicableToUnityVersionsSinceAttribute>())
                {
                    var instance = Activator.CreateInstance(handlerImpl);
                    foreach (var i in handlerImpl.GetInterfaces())
                    {
                        if (InterfacesOfInterest.Contains(i))
                        {
                            VersionedHandlers[i].Add((Version.Parse(startVersion.StartVersion), instance));
                        }
                    }
                }
            }

            foreach (var handlerList in VersionedHandlers.Values)
            {
                handlerList.Sort((a, b) => -a.Version.CompareTo(b.Version));
            }

            GatherUnityVersionSpecificHandlers();
        }

        private static void GatherUnityVersionSpecificHandlers()
        {
            CurrentHandlers.Clear();
            foreach (var type in InterfacesOfInterest)
            {
                foreach (var (version, handler) in VersionedHandlers[type])
                {
                    if (version > UnityVersion) continue;

                    CurrentHandlers[type] = handler;
                    break;
                }
            }
        }

        private static T GetHandler<T>()
        {
            if (CurrentHandlers.TryGetValue(typeof(T), out var result))
            {
                return (T)result;
            }

            Log.Error($"No direct for {typeof(T).FullName} found for Unity {UnityVersion}; this likely indicates a severe error somewhere");

            throw new ApplicationException("No handler");
        }

        private static Type[] GetAllTypesSafe()
        {
            try
            {
                return typeof(UseRightStructs).Assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException re)
            {
                return re.Types.Where(t => t != null).ToArray();
            }
        }

        internal static T GetStruct<T>(IntPtr ptr) where T : INativeStruct
        {
            if (ptr == IntPtr.Zero)
            {
                throw new ArgumentNullException("ptr");
            }

            var @struct = GetHandler<T>();

            @struct.Pointer = ptr;

            return @struct;
        }
    }
}
