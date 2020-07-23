using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Finance.Application.Actions.User.Register
{
    public class RegisterUserCommand : IRequest<RegisterUserResult>
    {
        public string Email { get; }
        public string Password { get; }

        public RegisterUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
