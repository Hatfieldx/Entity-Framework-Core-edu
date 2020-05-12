using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;

namespace custom_logger.Logger
{
    public class DbLogger : ILogger
    {
        private readonly string _path;

        static object _lock = new object();

        public DbLogger(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new InvalidOperationException("path mustn't be empty");
            }           
            
            _path = path;
        }
        public DbLogger()
        {
        }
        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel == LogLevel.Information;


        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_lock)
                {
                    File.AppendAllText(_path, formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }
}
