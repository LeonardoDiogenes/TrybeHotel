using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Services
{
    public class TokenGenerator
    {
        private readonly TokenOptions _tokenOptions;
        public TokenGenerator()
        {
           _tokenOptions = new TokenOptions {
                Secret = "4d82a63bbdc67c1e4784ed6587f3730c",
                ExpiresDay = 1
           };

        }
        public string Generate(UserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = AddClaims(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenOptions.Secret)),
                    SecurityAlgorithms.HmacSha256Signature),
                Subject = claims,
                Expires = System.DateTime.UtcNow.AddDays(_tokenOptions.ExpiresDay)  
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private ClaimsIdentity AddClaims(UserDto user)
        {
            var claims = new ClaimsIdentity();
            var clientClaim = new Claim(ClaimTypes.Email, user.Email!);
            claims.AddClaim(clientClaim);
            if (user.UserType == "admin")
            {
                var adminClaim = new Claim(ClaimTypes.Role, "admin");
                claims.AddClaim(adminClaim);
            }
            return claims;
        }
    }
}