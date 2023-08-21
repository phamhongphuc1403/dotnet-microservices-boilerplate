using TinyCRM.API.Extensions;
using TinyCRM.API.Middlewares;
using TinyCRM.EntityFrameworkCore;
using TinyCRM.EntityFrameworkCore.Logger;
using TinyCRM.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityExtension();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthenticationExtension(jwtSettings);

builder.Services.AddAddDbContextExtension(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddDependencyInjectionExtension();

builder.Services.AddSwaggerExtension();

builder.Services.AddAutoMapper(typeof(Mapper));
builder.Services.AddAutoMapper(typeof(IdentityMapper));

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