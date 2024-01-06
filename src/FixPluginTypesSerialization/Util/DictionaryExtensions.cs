using System;
using System.Collections.Generic;

namespace FixPluginTypesSerialization.Util
{
    internal static class DictionaryExtensions
    {
        public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> tuple, out T1 key, out T2 value)
        {
            key = tuple.Key;
            value = tuple.Value;
        }

        public static void Deconstruct(this VersionedHandler versionedHandler, out Version version, out object handler)
        {
            version = versionedHandler.version;
            handler = versionedHandler.handler;
        }
    }
}
