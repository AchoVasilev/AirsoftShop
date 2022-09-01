namespace AirsoftShop.Common.Services;

using Common;

public interface ICurrentUserService : IScopedService
{
    string? GetUserName();

    string? GetUserId();
}