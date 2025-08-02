using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Domain.Entities;
using DailyReport.Infrastructure.ClaimTypeContants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DailyReport.Infrastructure.Services
{
    public class ClaimToken : IClaimToken
    {
        public async Task<ClaimsIdentity> SetClaimFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            // Validate if the token is a valid JWT
            if (!handler.CanReadToken(token))
            {
                throw new ArgumentException("Invalid JWT token.");
            }

            var jwtToken = handler.ReadJwtToken(token);

            // Get claim by type
            var IdUser =  jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimConstant.IdUser)?.Value;
            var FullName = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimConstant.FullName)?.Value;
            var UserName = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimConstant.UserName)?.Value;
            var Role = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimConstant.Role)?.Value;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, Role),
                new Claim("FullName", FullName),
                new Claim("IdUser", IdUser),
                new Claim("Username", UserName),
                //new Claim("Role", Role),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24),
            };

            return claimsIdentity;
        }
    }
}
