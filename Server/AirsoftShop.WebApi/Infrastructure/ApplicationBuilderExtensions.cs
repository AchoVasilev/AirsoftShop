namespace AirsoftShop.WebApi.Infrastructure;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseRoutingAndAuth(this IApplicationBuilder app)
        => app.UseRouting()
            .UseAuthentication()
            .UseAuthorization();
}