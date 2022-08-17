namespace AirsoftShop.Services.Services.Identity;

public interface IIdentityService
{
    string GenerateJwtToken(string userId, string email, string jwtSettings);
}