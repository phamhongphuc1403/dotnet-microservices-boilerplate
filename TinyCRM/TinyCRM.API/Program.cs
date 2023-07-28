using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using TinyCRM.API.Modules.Account.Services;
using TinyCRM.API.Modules.Auth.Services;
using TinyCRM.API.Modules.Contact.Services;
using TinyCRM.API.Modules.Deal.Services;
using TinyCRM.API.Modules.DealProduct.Services;
using TinyCRM.API.Modules.Lead.Services;
using TinyCRM.API.Modules.Product.Services;
using TinyCRM.API.Modules.User.Services;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Middlewares;
using TinyCRM.Infrastructure.Database;
using TinyCRM.Infrastructure.Repositories;
using TinyCRM.Infrastructure.Repositories.Interfaces;
using TinyCRM.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); 
    options.Lockout.MaxFailedAccessAttempts = 5; 
    options.Lockout.AllowedForNewUsers = true;

    options.User.AllowedUserNameCharacters = 
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true; 


    options.SignIn.RequireConfirmedEmail = true;  
    options.SignIn.RequireConfirmedPhoneNumber = false; 
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromHours(1));

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, //The issuer is the actual server that created the token
        ValidateAudience = true, //The receiver of the token is a valid recipient
        ValidateLifetime = true, //The token has not expired
        ValidateIssuerSigningKey = true, //The signing key is valid and is trusted by the server
        ValidIssuer = jwtSettings["validIssuer"],
        ValidAudience = jwtSettings["validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(jwtSettings.GetSection("securityKey").Value))
    };
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ILeadService, LeadService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IDealService, DealService>();
builder.Services.AddScoped<IDealProductService, DealProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRepository<AccountEntity>, Repository<AccountEntity>>();
builder.Services.AddScoped<IRepository<ContactEntity>, Repository<ContactEntity>>();
builder.Services.AddScoped<IRepository<LeadEntity>, Repository<LeadEntity>>();
builder.Services.AddScoped<IRepository<ProductEntity>, Repository<ProductEntity>>();
builder.Services.AddScoped<IRepository<DealEntity>, Repository<DealEntity>>();
builder.Services.AddScoped<IRepository<DealProductEntity>, Repository<DealProductEntity>>();

builder.Services.AddScoped<Func<AppDbContext>>((provider) => () => provider.GetService<AppDbContext>());
builder.Services.AddScoped<DbFactory>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TinyCRM",
        Version = "v1"
    });
    option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Enter your token",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });

    option.OperationFilter<SecurityRequirementsOperationFilter>();
});

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