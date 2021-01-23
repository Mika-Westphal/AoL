using System;
using AoL.Api;
using AoL.Api.Plugin;
using HarmonyLib;

public class AoLController
{
    private static bool _isLoaded = false;
    public static AoL.Game Game { get; } = new AoL.Game();
    public static PluginLoader PluginLoader { get; } = new PluginLoader();
    public static void Init()
    {
        Game.Logger.Info("Init");
        if (_isLoaded) return;
        _isLoaded = true;
        AoLController aolController = new AoLController();
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
            Game.Logger.Info("Harmony Patching was sucessfully!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public const int AoLMajor = 0;
    public const int AoLMinor = 0;
    public const int AoLPatch = 1;
    public const string AoLVersion = "0.0.1";
}