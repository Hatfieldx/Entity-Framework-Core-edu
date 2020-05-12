using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;


namespace custom_logger.Logger
{
    public class DbLoggerProvider : ILoggerProvider
    {
        bool disposed = false;
        private readonly string _path;

        public DbLoggerProvider(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new InvalidOperationException("path mustn't be empty");
            }

            _path = path;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
//some disposible logic
            }

            disposed = true;
        }
        ~DbLoggerProvider()
        {
            Dispose(false);
        }
    }    
}
