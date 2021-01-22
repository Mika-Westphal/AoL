﻿using System;
using System.IO;

namespace AoL
{
    public class Game
    {
        public FileLocations Files { get; } = new FileLocations();
    }

    public class FileLocations
    {
        private string _aolDirectory;
        private string _aolPluginDirectory;

        public FileLocations()
        {
            _aolDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\AoL";
            _aolPluginDirectory = $"{_aolDirectory}\\plugins";
        }

        public string AolDirectory
        {
            get
            {
                if (!Directory.Exists(_aolDirectory))
                    Directory.CreateDirectory(_aolDirectory);

                return _aolDirectory;
            }   
        }

        public string PluginDirectory
        {
            get
            {
                if (!Directory.Exists(_aolPluginDirectory))
                    Directory.CreateDirectory(_aolPluginDirectory);

                return _aolPluginDirectory;
            }
        }
    }
}