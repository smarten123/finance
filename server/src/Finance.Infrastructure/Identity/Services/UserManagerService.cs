using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.Common.Interfaces;
using Finance.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Finance.Infrastructure.Identity.Services
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserManager(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> CreateAsync(string email, string password)
        {
            var userToCreate = new IdentityUser { UserName = email, Email = email };
            var registrationResult = await _userManager.CreateAsync(userToCreate, password);

            if (!registrationResult.Succeeded)
            {
                var errors = registrationResult.Errors
                    .Select(error => new Error(error.Code, error.Description))
                    .ToList();
                return Result.Failure(errors);
            }
            
            return Result.Success();
        }
    }
}
