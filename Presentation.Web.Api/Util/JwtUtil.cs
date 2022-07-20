using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Web.Api.Util
{
    public class JwtUtil : IJwtUtil
    {
        private readonly IConfiguration _configuration;

        public JwtUtil(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(int userId, string firstName, string lastName, string username)
        {
            var issuer = _configuration["JwtBearer:Issuer"];
            var audience = _configuration["JwtBearer:Audience"];
            var lifetime = _configuration.GetValue<int>("JwtBearer:Lifetime");

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtBearer:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);
            var claims = new[]
            {
                new Claim(ClaimTypes.Sid, $"{userId}"),
                new Claim(ClaimTypes.Name, $"{firstName} {lastName}"),
                new Claim(ClaimTypes.Email,  username)
            };

            var payload = new JwtPayload(issuer, audience, claims, DateTime.UtcNow, DateTime.UtcNow.AddHours(lifetime));
            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ValidateToken(string jwtBearer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtBearer:SecretKey"]);
            var issuer = _configuration["JwtBearer:Issuer"];
            var audience = _configuration["JwtBearer:Audience"];

            tokenHandler.ValidateToken(jwtBearer, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtSecurityToken = (JwtSecurityToken)validatedToken;
            var userId = jwtSecurityToken.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;

            return userId;
        }
    }
}