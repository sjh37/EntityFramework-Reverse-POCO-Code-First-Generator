using System;
using System.Reflection;

namespace Efrpg
{





    // ****************************************************************************************************************************************
    // **************************** No more settings to adjust. Rest of the generator code below. *********************************************
    // ****************************************************************************************************************************************

    public static class AssemblyHelper
    {
        public static object LoadPlugin(string assemblyAndType)
        {
            var assemblyInfo = assemblyAndType.Split(',');
            return LoadPlugin(assemblyInfo[0], assemblyInfo[1]);
        }

        /// <summary>
        /// Load a plugin
        /// </summary>
        /// <param name="assemblyFile">Full path including DLL name. i.e. "C:\\S\\Source (open source)\\EntityFramework Reverse POCO Code Generator\\Generator.Tests.Unit\\\bin\\Debug\\Generator.Tests.Unit.dll"</param>
        /// <param name="typeName">Fully qualified class name, including any namespaces. i.e. "Generator.Tests.Unit.MultiContextSettingsPlugin"</param>
        /// <returns></returns>
        public static object LoadPlugin(string assemblyFile, string typeName)
        {
            var asm = Assembly.LoadFrom(assemblyFile.Trim());
            var dynType = asm.GetType(typeName.Trim(), true, false);
            return Activator.CreateInstance(dynType);
        }
    }
}