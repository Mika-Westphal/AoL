using System;
using System.IO;
using System.Reflection;

namespace AoL.Api
{
    public class Logger
    {
        private readonly string _logPath = $"{AoLController.Game.Files.AoLDirectory}/latest.txt";
        public static Logger Get => AoLController.Game.Logger;
        
        internal Logger() {}

        public void Info(string message) => Info((object) message);
        
        public void Info(object message)
        {
            message = $"[Info/{Assembly.GetCallingAssembly().GetName().Name}/{DateTime.Now}] {message}";
            File.AppendAllText(_logPath, message + "\n");
            Console.WriteLine(message);
        }
        
        public void Warn(string message) => Warn((object) message);
        
        public void Warn(object message)
        {
            message = $"[Info/{Assembly.GetCallingAssembly().GetName().Name}/{DateTime.Now}] {message}";
            File.AppendAllText(_logPath, message + "\n");
            Console.WriteLine(message);
        }
        
        public void Error(string message) => Error((object) message);
        
        public void Error(object message)
        {
            message = $"[Info/{Assembly.GetCallingAssembly().GetName().Name}/{DateTime.Now}] {message}";
            File.AppendAllText(_logPath, message + "\n");
            Console.WriteLine(message);
        }

        public void Send(string message)
        {
            File.AppendAllText(_logPath, message + "\n");
            Console.WriteLine(message);
        }
    }
}