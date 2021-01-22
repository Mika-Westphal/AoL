using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AoL.Api.Plugin
{
    public class PluginLoader
    {
        private readonly List<IPlugin> _plugins = new List<IPlugin>();
        private readonly List<PluginLoadContext> _contexts = new List<PluginLoadContext>();
        private readonly List<PluginInformation> Plugins = new List<PluginInformation>();

        internal void ActivatePlugins()
        {
            AoLController.Game.Logger.Info("AoLController.Game.Files.PluginDirectory");
            List<string> paths = Directory.GetFiles(AoLController.Game.Files.PluginDirectory, "*.dll").ToList();
            Dictionary<PluginInformation, KeyValuePair<Type, List<Type>>> dictionary = new Dictionary<PluginInformation, KeyValuePair<Type, List<Type>>>();
            _contexts.Clear();
            foreach(var pluginpath in paths)
            {
                try
                {
                    var assembly = Assembly.Load(File.ReadAllBytes(pluginpath));
                    foreach (var type in assembly.GetTypes())
                    {
                        if (!typeof(IPlugin).IsAssignableFrom(type))
                            continue;

                        var infos = type.GetCustomAttribute<PluginInformation>();

                        if (infos == null)
                        {
                            AoLController.Game.Logger.Info($"The File {assembly.GetName().Name} has a class which inherits from IPlugin but has no PluginInformation ... Default Values will be added");
                            infos = new PluginInformation();
                        }

                        var allTypes = assembly.GetTypes().ToList();
                        allTypes.Remove(type);
                        dictionary.Add(infos, new KeyValuePair<Type, List<Type>>(type, allTypes));
                        break;
                    }
                }
                catch(Exception e)
                {
                    AoLController.Game.Logger.Error($"Synapse-Controller: Loading Assembly of {pluginpath} failed!!\n{e}");
                }
            }
            
            foreach (var infoTypePair in dictionary.OrderByDescending(x => x.Key.LoadPriority))
                try
                {
                    AoLController.Game.Logger.Info($"{infoTypePair.Key.Name} will now be activated!");

                    IPlugin plugin = (IPlugin)Activator.CreateInstance(infoTypePair.Value.Key);
                    plugin.Information = infoTypePair.Key;
                    plugin.PluginDirectory = AoLController.Game.Files.PluginDirectory;
                    _contexts.Add(new PluginLoadContext(plugin, infoTypePair.Value.Key, infoTypePair.Key, infoTypePair.Value.Value));
                    _plugins.Add(plugin);
                    Plugins.Add(infoTypePair.Key);
                }
                catch(Exception e) 
                {
                    AoLController.Game.Logger.Error($"Synapse-Controller: Activation of {infoTypePair.Value.Key.Assembly.GetName().Name} failed!!\n{e}");
                }

            LoadPlugins();
        }
        
        private void LoadPlugins()
        {
            foreach (IPlugin plugin in _plugins)
                try
                {
                    plugin.Load();
                }
                catch (Exception e)
                {
                    AoLController.Game.Logger.Error($"Synapse-Loader: {plugin.Information.Name} Loading failed!!\n{e}");
                }
        }
    }
    
    public class PluginLoadContext
    {
        internal PluginLoadContext(IPlugin plugin, Type type, PluginInformation pluginInformation, List<Type> classes)
        {
            Plugin = plugin;
            PluginType = type;
            Information = pluginInformation;
            Classes = classes;
        }

        public readonly IPlugin Plugin;
        public readonly Type PluginType;
        public readonly List<Type> Classes;
        public readonly PluginInformation Information;
    }
}