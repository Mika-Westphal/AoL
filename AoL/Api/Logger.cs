using System;
using System.IO;
using System.Reflection;

namespace AoL.Api
{
    public class Logger
    {
        public static Logger Get => AoLController.Game.Logger;
        private static string _logPath = Path.Combine(new FileLocations().LogsDirectory, "latest.txt");
        
        public void Info(string message) => Info((object) message);
        
        public void Info(object message)
        {
            Console.WriteLine($"[Info/{Assembly.GetCallingAssembly().GetName().Name}/{DateTime.Now}] {message}");
            File.AppendAllText(_logPath, $"[Info/{Assembly.GetCallingAssembly().GetName().Name}/{DateTime.Now}] {message}\n");
        }
        
        public void Warn(string message) => Warn((object) message);
        
        public void Warn(object message)
        {
            Console.WriteLine($"[Warn/{Assembly.GetCallingAssembly().GetName().Name}/{DateTime.Now}] {message}");
            File.AppendAllText(_logPath, $"[Warn/{Assembly.GetCallingAssembly().GetName().Name}/{DateTime.Now}] {message}\n");
        }
        
        public void Error(string message) => Error((object) message);
        
        public void Error(object message)
        {
            Console.WriteLine($"[Error/{Assembly.GetCallingAssembly().GetName().Name}/{DateTime.Now}] {message}");
            File.AppendAllText(_logPath, $"[Error/{Assembly.GetCallingAssembly().GetName().Name}/{DateTime.Now}] {message}\n");
        }

        public void Send(string message)
        {
            Console.WriteLine(message);
            File.AppendAllText(_logPath, message + "\n");
        }
    }
}