using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.MainModule.DTO;
using Application.MainModule.DTO.Request;
using Application.MainModule.DTO.Response;
using Application.MainModule.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public SecurityController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [Route("auth")]
        [HttpPost]
        public IActionResult SignIn(SignInRequestDto request)
        {
            if (ModelState.IsValid)
            {
                var userDto = _userService.SignIn(request.Username, request.Password);
                if (userDto == null) return NotFound();

                return Ok(BuildBearerToken(userDto));

            }

            return null;
        }

        private SignInResponseDto BuildBearerToken(UserDto userDto)
        {
            var issuer = _configuration["JwtBearer:Issuer"];
            var audience = _configuration["JwtBearer:Audience"];
            var lifetime = _configuration.GetValue<int>("JwtBearer:Lifetime");
            var secretKey = _configuration["JwtBearer:SecretKey"];

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(signingCredentials);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, $"{userDto.FirstName} {userDto.LastName}"),
                new Claim(ClaimTypes.Email, userDto.Username),
                new Claim(ClaimTypes.Sid, userDto.Id.ToString())
            };

            var payload = new JwtPayload(issuer, audience, claims, DateTime.Now, DateTime.Now.AddHours(lifetime));
            var token = new JwtSecurityToken(header, payload);

            return new SignInResponseDto { Token = new JwtSecurityTokenHandler().WriteToken(token), User = userDto };
        }
    }
}