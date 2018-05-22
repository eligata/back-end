using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.API.Helpers
{
    public class JwtConfigurator
    {
        public string SecretKey { get; set; }

        public string Issuer { get; set; }

        public string Subject { get; set; }

        public string Audience { get; set; }

        public TimeSpan ValidFor { get; set; }

        public DateTime NotBefore => DateTime.UtcNow;

        public DateTime IssuedAt => DateTime.UtcNow;

        public DateTime ExpiresAt => IssuedAt.Add(ValidFor);

        public string Jti { get { return Guid.NewGuid().ToString(); } }

        public SymmetricSecurityKey SymmetricSecurityKey { get; set; }

        public SigningCredentials SigningCredentials { get; set; }
    }

}
