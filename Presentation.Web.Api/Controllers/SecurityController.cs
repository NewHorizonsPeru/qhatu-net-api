using Application.MainModule.DTO;
using Application.MainModule.DTO.Request;
using Application.MainModule.DTO.Response;
using Application.MainModule.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Presentation.Web.Api.Util;

namespace Presentation.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IJwtUtil _jwtUtil;

        public SecurityController(IUserService userService, IConfiguration configuration, IJwtUtil jwtUtil)
        {
            _userService = userService;
            _configuration = configuration;
            _jwtUtil = jwtUtil;
        }

        [Route("auth")]
        [HttpPost]
        public IActionResult SignIn(SignInRequestDto request)
        {
            if (ModelState.IsValid)
            {
                var userDto = _userService.SignIn(request.Username, request.Password);
                if (userDto == null) return NotFound();
                var response = new SignInResponseDto
                {
                    Token = _jwtUtil.GenerateToken(userDto.Id, userDto.FirstName, userDto.LastName, userDto.Username),
                    User = userDto
                };
                return Ok(response) ;
            }

            return null;
        }

        [Route("signUp")]
        [HttpPost]
        public IActionResult SignUp(UserDto request)
        {
            return Ok(_userService.SignUp(request));
        }

    }
}