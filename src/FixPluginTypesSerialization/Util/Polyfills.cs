using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FixPluginTypesSerialization.Util
{
    internal static class Polyfills
    {
        public static bool StringIsNullOrWhiteSpace(string value)
        {
#if NET35
            if (value == null)
            {
                return true;
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }

            return true;
#else
            return string.IsNullOrWhiteSpace(value);
#endif
        }

        public static Version VersionParse(string input)
        {
#if NET35
            return new Version(input);
#else
            return Version.Parse(input);
#endif
        }

#if NET35 || NET40
        public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element) where T : Attribute
        {
            return (IEnumerable<T>)Attribute.GetCustomAttributes(element, typeof(T));
        }
#endif
    }
}
