using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoManger.Application.Services
{
    public class FileLogger
    {
        private readonly string _filePath;

        public FileLogger(string filePath = "log.txt")
        {
            _filePath = filePath;
        }

        public void Log(string action, string info)
        {
            string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {action} | {info}";

            using (StreamWriter writer = new StreamWriter(_filePath, append: true))
            {
                writer.WriteLine(line);
            }
        }
    }
}
