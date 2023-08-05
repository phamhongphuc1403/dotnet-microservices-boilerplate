using Microsoft.EntityFrameworkCore;
using Serilog;
using TinyCRM.API.Extensions;
using TinyCRM.API.Middlewares;
using TinyCRM.Infrastructure;
using TinyCRM.Infrastructure.Database;
using TinyCRM.Infrastructure.Logger;

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

builder.Services.AddAutoMapper(typeof(Mapper));

LoggerService.ConfigureLogger(builder.Configuration);
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