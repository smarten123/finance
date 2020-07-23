using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.Common.Models;

namespace Finance.Application.Actions.User.Register
{
    public class RegisterUserResult
    {
        public Result Result { get; }

        public RegisterUserResult(Result result)
        {
            Result = result;
        }
    }
}
