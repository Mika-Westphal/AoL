using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using AoL.Api.Plugin;
using HarmonyLib;
using Microsoft.Win32.SafeHandles;

public class AoLController
{
    private static bool _isLoaded = false;
    public static AoL.Game Game { get; } = new AoL.Game();
    private static PluginLoader PluginLoader { get; } = new PluginLoader();
    public static void Init()
    {
        CreateConsole();
        Console.WriteLine("Loaded");
        if (_isLoaded) return;
        _isLoaded = true;
        AoLController aoLController = new AoLController();
    }

    internal AoLController()
    {
        PatchMethods();
        PluginLoader.ActivatePlugins();
    }

    private void PatchMethods()
    {
        try
        {
            Harmony instance = new Harmony("aol.patches");
            instance.PatchAll();
            Console.WriteLine("Patching sucessfull");
        }
        catch (Exception)
        {
            // ignored
        }
    }
    
    // Token: 0x060007AF RID: 1967
    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int AllocConsole();

    // Token: 0x060007B0 RID: 1968
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, uint lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, uint hTemplateFile);

    // Token: 0x060007B1 RID: 1969
    public static void CreateConsole()
    {
        AllocConsole();
        FileStream stream = new FileStream(new SafeFileHandle(CreateFile("CONOUT$", 1073741824U, 2U, 0U, 3U, 0U, 0U), true), FileAccess.Write);
        Encoding encoding = Encoding.GetEncoding("UTF-8");
        Console.SetOut(new StreamWriter(stream, encoding)
        {
            AutoFlush = true
        });
        Console.SetError(new StreamWriter(stream, encoding)
        {
            AutoFlush = true
        });
    }

    // Token: 0x0400072D RID: 1837
    private const uint GENERIC_WRITE = 1073741824U;

    // Token: 0x0400072E RID: 1838
    private const uint FILE_SHARE_WRITE = 2U;

    // Token: 0x0400072F RID: 1839
    private const uint OPEN_EXISTING = 3U;
    
    public const int AoLMajor = 0;
    public const int AoLMinor = 0;
    public const int AoLPatch = 1;
}