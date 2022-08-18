namespace AirsoftShop.Common.Services;

using System.Security.Claims;
using Microsoft.AspNetCore.Http;

public class CurrentUserService : ICurrentUserService
{
    private readonly ClaimsPrincipal? user;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        => this.user = httpContextAccessor.HttpContext?.User;

    public string? GetUserName()
        => this.user?.Identity?.Name;

    public string? GetUserId()
        => this.user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
}