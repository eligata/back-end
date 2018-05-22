using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.API.Helpers
{
    public class TokenManager
    {
        #region Fields and Properties

        private readonly JwtConfigurator _jwtConfigurator;

        public JwtConfigurator JwtConfigurator { get { return _jwtConfigurator; } }

        #endregion

        #region CTOR

        public TokenManager(JwtConfigurator jwtConfigurator)
        {
            this._jwtConfigurator = jwtConfigurator;
        }

        #endregion

        /// <summary>
        /// Get JWT for user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetUserToken(IdentityUser user)
        {
            var jwToken = new
            {
                id = user.Id,
                auth_token = GenerateToken(user),
                expires_in = JwtConfigurator.ExpiresAt
            };

            return JsonConvert.SerializeObject(jwToken, new JsonSerializerSettings { Formatting = Formatting.None });
        }

        /// <summary>
        /// Generate user token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateToken(IdentityUser user)
        {
            List<Claim> userClaims = GetUserClaims(user);

            // Create the JWT security token and encode it.
            var jwtToken = new JwtSecurityToken(
                issuer: JwtConfigurator.Issuer,
                audience: JwtConfigurator.Audience,
                claims: userClaims,
                notBefore: JwtConfigurator.NotBefore,
                expires: JwtConfigurator.ExpiresAt,
                signingCredentials: JwtConfigurator.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        /// <summary>
        /// Get all user claims
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private List<Claim> GetUserClaims(IdentityUser user)
        {
            List<Claim> userClaims = new List<Claim>()
            {
                 new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                 new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                 new Claim(JwtRegisteredClaimNames.Jti, JwtConfigurator.Jti),
                 new Claim(JwtRegisteredClaimNames.Iat, JwtConfigurator.IssuedAt.ToString())
             };

            return userClaims;
        }
    }
}
