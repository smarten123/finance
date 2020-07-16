using System;
using Microsoft.IdentityModel.Tokens;

namespace Finance.Application.Auth
{
    public class TokenProviderOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public TimeSpan ExpiresAt { get; set; }
    }
}
