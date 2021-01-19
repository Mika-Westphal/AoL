using System;

namespace AoL.Api.Plugin
{
    public class PluginInformation : Attribute
    {
        public int SynapseMajor = AoLController.AoLMajor;
        public int SynapseMinor = AoLController.AoLMinor;
        public int SynapsePatch = AoLController.AoLPatch;
        public string Name = "Unknown";
        public string Author = "Unknown";
        public string Description = "Unknown";
        public string Version = "Unknown";
        public int LoadPriority = 0;

        internal bool shared = false;
    }
}