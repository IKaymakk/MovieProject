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
        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenResponseDto GenerateToken(UserDataDto entity)
        {
            // appsettings.json'dan ayarları okuma
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var key = _configuration["JwtSettings:Key"];
            var expireTime = int.Parse(_configuration["JwtSettings:ExpireTime"] ?? "1");

            if (string.IsNullOrEmpty(key)) throw new Exception("Key Null Olamaz");

            // Security key oluştur
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Claims tanımlama
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, entity.UserName!),
            new Claim(ClaimTypes.Role, entity.AppRoleId)
        };

            // Token süresi
            var expireDate = DateTime.UtcNow.AddHours(expireTime);

            // JWT oluşturma
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expireDate,
                signingCredentials: credentials
            );

            var handler = new JwtSecurityTokenHandler();
            return new TokenResponseDto(handler.WriteToken(token), expireDate);
        }
    }

}
