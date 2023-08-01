using Microsoft.EntityFrameworkCore;
using Serilog;
using TinyCRM.API.Extensions;
using TinyCRM.Domain.Middlewares;
using TinyCRM.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityExtension();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthenticationExtension(jwtSettings);

builder.Services.AddAuthorizationExtension();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDependencyInjectionExtension();

builder.Services.AddSwaggerExtension();

builder.Services.AddAutoMapper(typeof(Program));

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

var app = builder.Build();

app.UseHttpExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();