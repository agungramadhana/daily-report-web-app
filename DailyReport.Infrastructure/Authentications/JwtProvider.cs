using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Application;
using DailyReport.Application.Interfaces;
using DailyReport.Infrastructure.ClaimTypeContants;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DailyReport.Infrastructure.Authentications
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOption;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _jwtOption = options.Value;
        }

        public async Task<string> GenerateToken(UserModel user)
        {
            var claims = new Claim[]
            {
                new(ClaimConstant.IdUser, user.IdUser.ToString()),
                new(ClaimConstant.FullName, user.FullName!),
                new(ClaimConstant.UserName, user.UserName!),
                new(ClaimConstant.Role, user.Role!)
            };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.SecretKey!)),
                SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                _jwtOption.Issuer,
                _jwtOption.Audience,
                claims,
                null,
                DateTime.UtcNow.AddDays(7),
                credentials
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public string Hash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public bool Verify(string storedHash, string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                var computedHash = Convert.ToBase64String(hash);

                // Compare the computed hash with the stored hash
                return computedHash == storedHash;
            }
        }
    }
}
