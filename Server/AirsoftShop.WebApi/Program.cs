using AirsoftShop.WebApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .RegisterDatabase(builder.Configuration)
    .RegisterIdentity()
    .AddJwtAuthentication(builder.Services.GetJwtSettings(builder.Configuration))
    .RegisterApplicationServices()
    .AddControllers();

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