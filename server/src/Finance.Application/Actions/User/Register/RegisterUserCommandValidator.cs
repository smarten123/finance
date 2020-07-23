using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Finance.Application.Actions.User.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(user => user.Email).NotEmpty().WithMessage("An email is required.")
                .EmailAddress().WithMessage("An email with a correct format is required.");

            RuleFor(user => user.Password).NotEmpty().WithMessage("A password is required.");
        }
    }
}
