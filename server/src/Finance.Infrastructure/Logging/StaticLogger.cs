using System;
using Finance.Application.Common.Interfaces;

namespace Finance.Infrastructure.Logging
{
    public static class StaticLogger
    {
        private static IAppLogger _logger = new SilentLogger();

        public static IAppLogger Logger
        {
            get => _logger;
            set => _logger = value ?? throw new ArgumentException(nameof(value));
        }

        public static void Information(string message, params object[] args)
        {
            Logger.Information(message, args);
        }

        public static void Warning(string message, params object[] args)
        {
            Logger.Warning(message, args);
        }

        public static void Error(string message, params object[] args)
        {
            Logger.Error(message, args);
        }
    }
}
