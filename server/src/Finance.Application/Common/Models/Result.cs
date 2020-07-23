using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Common.Models
{
    public class Result
    {
        public bool Succeeded { get; }
        public IEnumerable<Error> Errors { get; }

        private Result(bool succeeded, IEnumerable<Error> errors)
        {
            Succeeded = succeeded;
            Errors = errors;
        }

        public static Result Success()
        {
            return new Result(true, new List<Error>());
        }

        public static Result Failure(IEnumerable<Error> errors)
        {
            return new Result(false, errors);
        }
    }
}
