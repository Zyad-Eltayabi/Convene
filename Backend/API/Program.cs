using API.Extensions;
using API.Middlewares;
using Application;
using Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddTransient<ExceptionMiddleware>();

WebApplication app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000", "https://localhost:3000"));


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Convene API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.InitializeDatabaseAsync();

app.Run();
