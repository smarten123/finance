using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Web.Models.User
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; } = null!;


        [Compare("Password", ErrorMessage = "The Password field and ConfirmPassword field must match.")]
        public string? ConfirmPassword { get; set; }
    }
}
