using CoolParking.BL.Interfaces;
using System;
using System.IO;

namespace CoolParking.BL.Services
{
    public class LogService : ILogService
    {
        private readonly string _logFilePath;

        public LogService(string logService)
        {
            _logFilePath = logService;
        }

        public string LogPath
        {
            get { return _logFilePath; }
        }

        public string Read()
        {
            string result = "";
            if (File.Exists(_logFilePath))
            {
                using (StreamReader streamReader = File.OpenText(_logFilePath))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        result += line + "\n";
                    }
                }
            }
            else
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public void Write(string logInfo)
        {
            File.AppendAllText(_logFilePath, logInfo + Environment.NewLine);
        }
    }
}