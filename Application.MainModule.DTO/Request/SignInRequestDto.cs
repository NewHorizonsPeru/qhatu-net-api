using System.ComponentModel.DataAnnotations;

namespace Application.MainModule.DTO.Request
{
    public class SignInRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}