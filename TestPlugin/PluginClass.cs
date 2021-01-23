using AoL.Api.Plugin;

namespace TestPlugin
{
    [PluginInformation(
        Name = "Test Plugin",
        Author = "Mika",
        Version = "1.0.0",
        Description = "Test Plugin",
        AoLMajor = 0,
        AoLMinor = 0,
        AoLPatch = 1)]
    public class PluginClass : AbstractPlugin
    {
        public override void Load()
        {
            AoLController.Game.Logger.Info("Hello from Test Plugin!");
        }
    }
}