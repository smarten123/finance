using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Common.Interfaces
{
    public interface IAppLogger
    {
        void Information(string message, params object[] args);

        void Warning(string message, params object[] args);

        void Error(string message, params object[] args);
    }
}
