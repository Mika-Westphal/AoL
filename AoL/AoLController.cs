using System;
using AoL.Api.Plugin;
using HarmonyLib;
public class AoLController
{
    private static bool _isLoaded = false;
    public static AoL.Game Game { get; } = new AoL.Game();
    private static PluginLoader PluginLoader { get; } = new PluginLoader();
    public static void Init()
    {
        Console.WriteLine("Loaded");
        if (_isLoaded) return;
        _isLoaded = true;
        AoLController aoLController = new AoLController();
    }

    private AoLController()
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
    public const int AoLMajor = 0;
    public const int AoLMinor = 0;
    public const int AoLPatch = 1;
}