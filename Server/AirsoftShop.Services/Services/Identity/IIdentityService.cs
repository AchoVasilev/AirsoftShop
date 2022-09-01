namespace AirsoftShop.Services.Services.Identity;

using Common;

public interface IIdentityService : IScopedService
{
    string GenerateJwtToken(string userId, string email, string jwtSettings);
}