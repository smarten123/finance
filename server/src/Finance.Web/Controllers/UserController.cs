using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.Actions.User.Register;
using Finance.Web.Models.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Web.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var command = new RegisterUserCommand(model.Email, model.Password);
            var result = await _mediator.Send(command);

            if (result.Result.Succeeded)
            {
                return NoContent();
            }

            foreach (var error in result.Result.Errors)
            {
                ModelState.AddModelError(error.Identifier, error.Description);
            }

            return ValidationProblem();
        }
    }
}
