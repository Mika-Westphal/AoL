﻿using System.IO;

namespace AoL.Api.Plugin
{
    public interface IPlugin
    {
        PluginInformation Information { get; set; }

        string PluginDirectory { get; set; }

        void Load();

        void ReloadConfigs();
    }


    public abstract class AbstractPlugin : IPlugin
    {
        private string _pluginDirectory;

        public virtual void Load() => Logger.Get.Info($"{Information.Name} by {Information.Author} has loaded!");

        public virtual void ReloadConfigs()
        {
        }

        public PluginInformation Information { get; set; }

        public string PluginDirectory
        {
            get
            {
                if (_pluginDirectory == null)
                    return null;

                if (!Directory.Exists(_pluginDirectory))
                    Directory.CreateDirectory(_pluginDirectory);

                return _pluginDirectory;
            }
            set => _pluginDirectory = value;
        } 
    }
}