namespace Application.MainModule.DTO.Response
{
    public class SignInResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}