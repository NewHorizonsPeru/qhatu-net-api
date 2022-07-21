namespace Infrastructure.CrossCutting.Jwt
{
    public interface IJwtManager
    {
        string GenerateToken(int userId, string firstName, string lastName, string username);
        string ValidateToken(string jwtBearer);
    }
}