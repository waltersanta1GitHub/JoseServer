using System.Text;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JosesServer.Util
{
    public class UtilCollection
    {
        public static string DATABASE_NAME = "testtaskdb";
        public static string CONNECTION = "mongodb://localhost:27017";
        private static readonly string SECRET_KEY = "Clave1234567890$   Clave1234567890$<=>Clave1234567890$   Clave1234567890$";
        private static int MINUTES_TO_EXPIRE_TOKEN = 180;

        private static readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

        private static readonly TokenValidationParameters ValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = SecurityKey,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.Zero,
        };

        public static bool ValidateToken(string token)
        {

            try
            {
                SecurityToken securityToken;
                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(token, ValidationParameters, out securityToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, username),
                }),
                Expires = DateTime.UtcNow.AddMinutes(MINUTES_TO_EXPIRE_TOKEN),
                SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
