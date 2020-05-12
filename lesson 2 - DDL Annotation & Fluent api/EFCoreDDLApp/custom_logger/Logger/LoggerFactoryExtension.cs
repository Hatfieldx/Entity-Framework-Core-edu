using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace custom_logger.Logger
{
    public static class LoggerFactoryExtension
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory, string path)
        {
            factory.AddProvider(new DbLoggerProvider(path));

            return factory;
        }
    }
}
