using Application.MainModule.DTO;
using Application.MainModule.DTO.Request;
using Application.MainModule.DTO.Response;
using Application.MainModule.IServices;
using Infrastructure.CrossCutting.Jwt;
using Infrastructure.CrossCutting.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Presentation.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IJwtManager _jwtManager;
        private readonly ILoggerManager _logger;

        public SecurityController(IUserService userService, IConfiguration configuration, IJwtManager jwtManager, ILoggerManager logger)
        {
            _userService = userService;
            _configuration = configuration;
            _jwtManager = jwtManager;
            _logger = logger;
        }

        [Route("auth")]
        [HttpPost]
        public IActionResult SignIn(SignInRequestDto request)
        {
            //throw new Exception("Error connecting database...");
            _logger.LoggerInfo("Ingresando al metodo de autenticación.");
            if (ModelState.IsValid)
            {
                var userDto = _userService.SignIn(request.Username, request.Password);
                if (userDto == null) return NotFound();
                var response = new SignInResponseDto
                {
                    Token = _jwtManager.GenerateToken(userDto.Id, userDto.FirstName, userDto.LastName, userDto.Username),
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