using System;
using System.Reflection;

namespace MonoMod
{
    internal static class AssemblyExtensions
    {
        /// <summary>
        /// Assembly.GetName() throws an error when called on an assembly with double byte characters in the filepath.
        /// E.g. if the user has characters with accents like 'Á' or 'ü', or non-Latin characters in their user directory.
        /// This method returns the name of the assembly without calling the GetName method to avoid this exception.
        /// (See this GitHub issue for discussion of the underlying bug: https://github.com/mono/mono/issues/20968 )
        /// </summary>
        /// <returns>The simple name of the assembly</returns>
        internal static string SafeGetName(this Assembly self)
        {
            string fullName = self.FullName;
            // e.g.: "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
            int nameLength = fullName.IndexOf(',');
            string name = fullName.Substring(0, nameLength);
            return name;
        }

        internal static AssemblyName SafeGetAssemblyName(this Assembly self)
        {
            return new AssemblyName(self.FullName);
        }

        /// <summary>
        /// Assembly.GetName() throws an error when called on an assembly with double byte characters in the filepath.
        /// E.g. if the user has characters with accents like 'Á' or 'ü', or non-Latin characters in their user directory.
        /// This method returns the name of the assembly without calling the GetName method to avoid this exception.
        /// (See this GitHub issue for discussion of the underlying bug: https://github.com/mono/mono/issues/20968 )
        /// </summary>
        /// <returns>The version of the assembly</returns>
        internal static Version SafeGetVersion(this Assembly self)
        {
            string fullName = self.FullName;
            // e.g.: "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
            string versionHeader = ", Version=";
            int versionHeaderIndex = fullName.IndexOf(versionHeader);
            int versionNumberIndex = versionHeaderIndex + versionHeader.Length;
            int versionCommaIndex = fullName.IndexOf(',', versionNumberIndex);
            int versionNumberLength = versionCommaIndex - versionNumberIndex;
            string versionNumber = fullName.Substring(versionNumberIndex, versionNumberLength);
            Version version = new Version(versionNumber);
            return version;
        }
    }
}
