namespace AirsoftShop.Services.Services.Identity;

using Common.Services.Common;

public interface IIdentityService : IScopedService
{
    string GenerateJwtToken(string userId, string email, string jwtSettings);
}