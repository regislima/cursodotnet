using System.Text;
using api.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Security.Claims;

namespace api.Util.Security
{
    public static class CryptoFunctions
    {
        public static string GenerateToken(IConfiguration configuration, User user)
        {
            int tokenExpiredTimeLapse = int.Parse(configuration["TokenExpireTimeLapse"]);
            byte[] encoding = Encoding.UTF8.GetBytes(configuration["SecurityKey"]);

            SymmetricSecurityKey key = new SymmetricSecurityKey(encoding);
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken token = new JwtSecurityToken
            (
                issuer: configuration["Issuer"],
                audience: configuration["Audience"],
                expires: DateTime.Now.AddMinutes(tokenExpiredTimeLapse),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
