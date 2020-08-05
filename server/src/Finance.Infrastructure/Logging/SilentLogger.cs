using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.Common.Interfaces;

namespace Finance.Infrastructure.Logging
{
    internal class SilentLogger : IAppLogger
    {
        public void Information(string message, params object[] args)
        {
        }

        public void Warning(string message, params object[] args)
        {
        }

        public void Error(string message, params object[] args)
        {
        }
    }
}
