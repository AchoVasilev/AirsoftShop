namespace AirsoftShop.Common.Services;

public interface ICurrentUserService
{
    string? GetUserName();

    string? GetUserId();
}