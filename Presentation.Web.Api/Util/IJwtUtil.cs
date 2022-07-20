namespace Presentation.Web.Api.Util
{
    public interface IJwtUtil
    {
        string GenerateToken(int userId, string firstName, string lastName, string username);
        string ValidateToken(string jwtBearer);
    }
}