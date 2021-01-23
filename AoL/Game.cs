using System;
using System.IO;
using AoL.Api;

namespace AoL
{
    public class Game
    {
        public static Game Get => AoLController.Game;
        public FileLocations Files { get; }
        public Logger Logger { get; }

        public Game()
        {
            Files = new FileLocations();
            Logger = new Logger();
        }
    }

    public class FileLocations
    {
        private string _aolDirectory;
        private string _aolPluginDirectory;
        private string _aolLogsDirectory;

        public FileLocations()
        {
            _aolDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AoL");
            _aolPluginDirectory = Path.Combine(_aolDirectory, "plugins");
            _aolLogsDirectory = Path.Combine(_aolDirectory, "logs");
        }

        public string AoLDirectory
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
        
        public string LogsDirectory
        {
            get
            {
                if (!Directory.Exists(_aolLogsDirectory))
                    Directory.CreateDirectory(_aolLogsDirectory);

                return _aolLogsDirectory;
            }
        }
    }
}