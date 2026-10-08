using API.Extensions;
using API.Middlewares;
using Application;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Persistence.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();

builder.Services.AddControllers(opt =>
{
    AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddIdentityApiEndpoints<User>(opt =>
{
    opt.User.RequireUniqueEmail = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("IsActivityHost", policy =>
    {
        policy.Requirements.Add(new IsHostRequirement());
    });
});

builder.Services.AddTransient<IAuthorizationHandler, IsHostRequirementHandler>();

WebApplication app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod()
.AllowCredentials()
.WithOrigins("http://localhost:3000", "https://localhost:3000"));


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGroup("api")
    .MapIdentityApi<User>();
// pause the database initialization for now, as it may not be needed in all environments
await app.InitializeDatabaseAsync();

app.Run();
