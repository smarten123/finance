using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Serilog.ILogger;

namespace Finance.Infrastructure.Logging
{
    public class SerilogLogAdapter : IAppLogger
    {
        private readonly ILogger _logger;

        public SerilogLogAdapter(ILogger logger)
        {
            _logger = logger;
        }

        public void Information(string message, params object[] args)
        {
            _logger.Information(message, args);
        }

        public void Warning(string message, params object[] args)
        {
            _logger.Warning(message, args);
        }

        public void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }
    }
}
