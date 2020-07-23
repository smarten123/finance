using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Finance.Application.Common.Interfaces;
using MediatR;

namespace Finance.Application.Actions.User.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResult>
    {
        private readonly IUserManager _userManager;

        public RegisterUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _userManager.CreateAsync(request.Email, request.Password);
            return new RegisterUserResult(result);
        }
    }
}
