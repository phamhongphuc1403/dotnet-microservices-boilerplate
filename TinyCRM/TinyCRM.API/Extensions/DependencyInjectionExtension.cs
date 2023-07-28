using TinyCRM.API.Modules.Account.Services;
using TinyCRM.API.Modules.Auth.Services;
using TinyCRM.API.Modules.Contact.Services;
using TinyCRM.API.Modules.Deal.Services;
using TinyCRM.API.Modules.DealProduct.Services;
using TinyCRM.API.Modules.Lead.Services;
using TinyCRM.API.Modules.Product.Services;
using TinyCRM.API.Modules.User.Services;
using TinyCRM.Domain.Entities;
using TinyCRM.Infrastructure.Database;
using TinyCRM.Infrastructure.Repositories.Interfaces;
using TinyCRM.Infrastructure.Repositories;
using TinyCRM.Infrastructure.UnitOfWork;

namespace TinyCRM.API.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjectionExtension(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ILeadService, LeadService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IDealService, DealService>();
            services.AddScoped<IDealProductService, DealProductService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRepository<AccountEntity>, Repository<AccountEntity>>();
            services.AddScoped<IRepository<ContactEntity>, Repository<ContactEntity>>();
            services.AddScoped<IRepository<LeadEntity>, Repository<LeadEntity>>();
            services.AddScoped<IRepository<ProductEntity>, Repository<ProductEntity>>();
            services.AddScoped<IRepository<DealEntity>, Repository<DealEntity>>();
            services.AddScoped<IRepository<DealProductEntity>, Repository<DealProductEntity>>();

            services.AddScoped<Func<AppDbContext>>((provider) => () => provider.GetService<AppDbContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
