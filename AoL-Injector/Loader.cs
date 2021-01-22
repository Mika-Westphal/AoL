using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace AoL_Injector
{
    public class Loader
    {
        /// <summary>
        /// Entrypoint for Synapse
        /// </summary>
        public static void LoadSystem()
        {
            CreateConsole();
            string aol = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AoL");
            string logPath = Path.Combine(aol, "logs");
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            else if (File.Exists(logPath + "/latest.txt"))
            {
                File.Move(logPath + "/latest.txt", logPath + $"/latest-{DateTime.Now:MM-dd-yy-h-mm-ss}.txt");
            }
            if (!Directory.Exists(aol))
            {
                Directory.CreateDirectory(aol);
            }
            List<Assembly> dependencyAssemblies = new List<Assembly>();
            string[] files = Directory.GetFiles(Path.Combine(aol, "dependencies"));
            foreach (string t in files)
            {
                Assembly assembly = Assembly.Load(File.ReadAllBytes(t));
                dependencyAssemblies.Add(assembly);
            }
            Assembly assembly2 = Assembly.Load(File.ReadAllBytes(Path.Combine(aol, "AoL.dll")));
            printBanner(logPath, assembly2, dependencyAssemblies);
            InvokeAssembly(assembly2);
        }

        /// <summary>
        /// Print Synapse Banner and Version Information
        /// </summary>
        private static void printBanner(string logPath, Assembly syn, List<Assembly> dep)
        {
            string banner = "\nLoading Synapse...\n" +
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
                            string.Join("\n",
                                dep.Select(assembly => $"{assembly.GetName().Name}: {assembly.GetName().Version}")
                                    .ToList()) + "\n" +
                            "-------------------===Loader===-------------------";
            Console.WriteLine(banner);
            File.AppendAllText(logPath + "/latest.txt", banner);
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
                    .First((Type t) => t.Name == "SynapseController").GetMethods()
                    .First((MethodInfo m) => m.Name == "Init")
                    .Invoke(null, null);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }
        
        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int AllocConsole();

        // Token: 0x0600089E RID: 2206
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, uint lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, uint hTemplateFile);

        // Token: 0x0600089F RID: 2207
        public static void CreateConsole()
        {
            Loader.AllocConsole();
            Stream stream = new FileStream(new SafeFileHandle(Loader.CreateFile("CONOUT$", 1073741824U, 2U, 0U, 3U, 0U, 0U), true), FileAccess.Write);
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            Console.SetOut(new StreamWriter(stream, encoding)
            {
                AutoFlush = true
            });
        }

        // Token: 0x040007A4 RID: 1956
        private const uint GENERIC_WRITE = 1073741824U;

        // Token: 0x040007A5 RID: 1957
        private const uint FILE_SHARE_WRITE = 2U;

        // Token: 0x040007A6 RID: 1958
        private const uint OPEN_EXISTING = 3U;
    }
}