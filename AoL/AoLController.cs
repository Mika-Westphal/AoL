using System;
using System.IO;
using AoL;
using HarmonyLib;

public class AoLController
{
    private static bool IsLoaded = false;
    public static void Init()
    {
        File.Create("E:\\AoLLaunched");
        Console.WriteLine("Test");
        File.Create("E:\\AoLLaunched2");
        if (IsLoaded) return;
        IsLoaded = true;
        var aolController = new AoLController();
    }

    internal AoLController()
    {
        Console.WriteLine("Private");
        PatchMethods();
    }

    private void PatchMethods()
    {
        try
        {
            Harmony instance = new Harmony("aol.patches");
            instance.PatchAll();
            Console.WriteLine("Harmony Patching was sucessfully!");
            Console.WriteLine(new FileLocations().PluginDirectory);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public const int AoLMajor = 0;
    public const int AoLMinor = 0;
    public const int AoLPatch = 1;
    public const string AoLVersion = "0.0.1";
}