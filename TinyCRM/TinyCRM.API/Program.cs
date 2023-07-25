using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using TinyCRM.API.Modules.Account.Services;
using TinyCRM.API.Modules.Contact.Services;
using TinyCRM.API.Modules.Deal.Services;
using TinyCRM.API.Modules.DealProduct.Services;
using TinyCRM.API.Modules.Lead.Services;
using TinyCRM.API.Modules.Product.Services;
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