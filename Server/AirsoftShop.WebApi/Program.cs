using AirsoftShop.WebApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .RegisterDatabase(builder.Configuration)
    .RegisterIdentity()
    .AddSwagger()
    .AddJwtAuthentication(builder.Services.GetJwtSettings(builder.Configuration))
    .RegisterCloudinary(builder.Configuration)
    .RegisterApplicationServices()
    .AddControllers(options =>
    {
        options.Filters.Add<OperationCancelledExceptionFilter>();
    });

var app = builder.Build();

await app.PrepareDatabase();

app.UseSwaggerUi()
    .UseRoutingAndAuth();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllers();

app.Run();