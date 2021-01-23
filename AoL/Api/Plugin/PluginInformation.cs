using System;

namespace AoL.Api.Plugin
{
    public class PluginInformation : Attribute
    {
        public int AoLMajor = AoLController.AoLMajor;
        public int AoLMinor = AoLController.AoLMinor;
        public int AoLPatch = AoLController.AoLPatch;
        public string Name = "Unknown";
        public string Author = "Unknown";
        public string Description = "Unknown";
        public string Version = "Unknown";
        public int LoadPriority = 0;
    }
}