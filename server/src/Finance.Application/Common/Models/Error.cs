using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Common.Models
{
    public class Error
    {
        public string Identifier { get; }
        public string Description { get; }

        public Error(string identifier, string description)
        {
            Identifier = identifier;
            Description = description;
        }
    }
}
