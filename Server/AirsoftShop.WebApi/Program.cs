using AirsoftShop.Common.Services;
using AirsoftShop.Services.Services.Category;
using AirsoftShop.WebApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .RegisterDatabase(builder.Configuration)
    .RegisterIdentity()
    .AddJwtAuthentication(builder.Services.GetJwtSettings(builder.Configuration))
    .AddControllers();

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>()
    .AddTransient<ICategoryService, CategoryService>();

var app = builder.Build();

await app.PrepareDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRoutingAndAuth();

app.MapControllers();

app.Run();