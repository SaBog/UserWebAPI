using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserWebAPI.DTO;
using UserWebAPI.Services;
using UserWebAPI.Services.Interfaces;

namespace UserWebAPI.Services
{
    public class JwtService : IJwtService
    {
        public string GetJwtToken(UserDto user)
        {
            var identity = GetIdentity(user);
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new (AuthOptions.GetSymmetricSecurityKey(), 
                        SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        private static ClaimsIdentity GetIdentity(UserDto user)
        {
            var role = user.Roles.Last().Name;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };

            ClaimsIdentity claimsIdentity = new (claims, "GetJwtToken", 
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}