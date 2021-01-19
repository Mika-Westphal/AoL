using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AoL_Injector
{
    public class Loader
    {
        [DllImport("kernel32")]
        static extern bool AllocConsole();
        /// <summary>
        /// Entrypoint for Synapse
        /// </summary>
        public static void LoadSystem()
        {
            AllocConsole();
            var aol = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AoL");
            if (!Directory.Exists(aol)) Directory.CreateDirectory(aol);
            
            var dependencyAssemblies = new List<Assembly>();
            foreach (var depend in Directory.GetFiles(Path.Combine(aol, "dependencies")))
            {
                var assembly = Assembly.Load(File.ReadAllBytes(depend));
                dependencyAssemblies.Add(assembly);
            };

            var aolAssembly = Assembly.Load(File.ReadAllBytes(Path.Combine(aol, "AoL.dll")));
            
            printBanner(aolAssembly, dependencyAssemblies);
            
            InvokeAssembly(aolAssembly);
        }

        /// <summary>
        /// Print Synapse Banner and Version Information
        /// </summary>
        private static void printBanner(Assembly syn, List<Assembly> dep)
        {
            Console.WriteLine(
                "\nLoading Synapse...\n" +
                "-------------------===Loader===-------------------\n" +
                "               _      \n" +
                "    /\\        | |     \n" +
                "   /  \\   ___ | |     \n" +
                "  / /\\ \\ / _ \\| |     \n" +
                " / ____ \\ (_) | |____ \n" +
                "/_/    \\_\\___/|______|\n" +
                $"SynapseVersion {syn.GetName().Version}\n" +
                $"LoaderVersion: 1.0.0.0\n" +
                $"RuntimeVersion: {Assembly.GetExecutingAssembly().ImageRuntimeVersion}\n\n" +
                string.Join("\n", dep.Select(assembly => $"{assembly.GetName().Name}: {assembly.GetName().Version}").ToList()) + "\n" +
                "-------------------===Loader===-------------------");

        }

        /// <summary>
        /// Scan assembly for synapse main class
        /// and invoke the init method 
        /// </summary>
        /// <param name="assembly">The Assembly Object</param>
        private static void InvokeAssembly(Assembly assembly)
        {
            try
            {
                assembly.GetTypes()
                    .First((Type t) => t.Name == "AoLController").GetMethods()
                    .First((MethodInfo m) => m.Name == "Init")
                    .Invoke(null, null);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }
    }
}