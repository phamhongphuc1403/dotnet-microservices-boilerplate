using Microsoft.AspNetCore.Authorization;
using TinyCRM.Application.Common.Interfaces;
using TinyCRM.Application.Modules.Account.Services;
using TinyCRM.Application.Modules.Account.Services.Interfaces;
using TinyCRM.Application.Modules.Auth.Services;
using TinyCRM.Application.Modules.Auth.Services.Interfaces;
using TinyCRM.Application.Modules.Contact.Services;
using TinyCRM.Application.Modules.Contact.Services.Interfaces;
using TinyCRM.Application.Modules.Deal.Services;
using TinyCRM.Application.Modules.Deal.Services.Interfaces;
using TinyCRM.Application.Modules.DealProduct.Services;
using TinyCRM.Application.Modules.DealProduct.Services.Interfaces;
using TinyCRM.Application.Modules.Lead.Services;
using TinyCRM.Application.Modules.Lead.Services.Interfaces;
using TinyCRM.Application.Modules.Product.Services;
using TinyCRM.Application.Modules.Product.Services.Interfaces;
using TinyCRM.Application.Modules.User.Services;
using TinyCRM.Application.Modules.User.Services.Interfaces;
using TinyCRM.Domain;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Repositories;
using TinyCRM.Infrastructure.Database;
using TinyCRM.Infrastructure.Identity.Services;
using TinyCRM.Infrastructure.Identity.Services.Interfaces;
using TinyCRM.Infrastructure.JWT.Services;
using TinyCRM.Infrastructure.Repositories;

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
            services.AddScoped<IDealRepository, DealRepository>();

            services.AddScoped<IAuthorizationHandler, ViewOrUpdateUserHandler>();

            services.AddScoped<Func<AppDbContext>>((provider) => () => provider.GetService<AppDbContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IIdentityAuthService, IdentityAuthService>();
            services.AddScoped<IIdentityRoleService, IdentityRoleService>();
            services.AddScoped<IIdentityHelper, IdentityHelper>();

            return services;
        }
    }
}