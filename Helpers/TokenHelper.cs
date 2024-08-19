using DirectoryPermissionManagement.DTOs;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Configuration;
using DirectoryPermissionManagement.Configs;

namespace DirectoryPermissionManagement.Helpers
{
    public class TokenHelper
    {

        // To generate token
        public string GenerateToken(UserResponse userResponse, JWTConfig config)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userResponse.Name),
                new Claim("id", userResponse.Id.ToString())
            };
            var token = new JwtSecurityToken(config.Issuer,
                config.Atudience,
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
