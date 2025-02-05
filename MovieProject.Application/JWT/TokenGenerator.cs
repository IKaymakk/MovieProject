using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieProject.Application.DTOS;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Application.JWT
{
    public class TokenGenerator
    {
        private static IConfiguration? _configuration;
        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static TokenResponseDto GenerateToken(UserDataDto entity)
        {
            // appsettings.json'dan ayarları okuma
            if (JwtDefaults.Key == null) throw new Exception("Key Null Olamaz");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtDefaults.Key!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, entity.UserName!),
                new Claim(ClaimTypes.Role, entity.Role)
            };

            var expireDate = DateTime.UtcNow.AddHours(JwtDefaults.ExpireTime);

            var token = new JwtSecurityToken(
               issuer: JwtDefaults.Issuer,
               audience: JwtDefaults.Audience,
               claims: claim,
               expires: expireDate,
               signingCredentials: credentials
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return new TokenResponseDto(handler.WriteToken(token), expireDate);

        }
    }

}
