using AirsoftShop.Common.Services;
using AirsoftShop.WebApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .RegisterDatabase(builder.Configuration)
    .RegisterIdentity()
    .AddControllers();

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

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