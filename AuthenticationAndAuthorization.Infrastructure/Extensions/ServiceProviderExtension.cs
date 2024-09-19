using AuthenticationAndAuthorization.Domain.Context;
using AuthenticationAndAuthorization.Infrastructure.Repositories.Implementations;
using AuthenticationAndAuthorization.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AuthenticationAndAuthorization.Infrastructure.Extensions
{
    public static class ServiceProviderExtension
    {
        public static IdentityBuilder AddIdentity(this IServiceCollection services)
        {
            return services
                .AddIdentity<IdentityUser, IdentityRole>(options =>
                    {
                        options.SignIn.RequireConfirmedAccount = true;
                        options.Password.RequiredLength = 10;
                        options.Password.RequiredUniqueChars = 3;
                        options.Password.RequireNonAlphanumeric = false;
                    })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }

        public static IServiceCollection AddDbContexts(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            return
                services
                    .AddDbContext<AppDbContext>(options =>
                    {
                        if (!options.IsConfigured)
                        {
                            options
                                .UseLazyLoadingProxies()
                                .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning))
                                .UseSqlServer(configurationManager.GetConnectionString("DefaultConnection"));
                        }
                    });
        }

        public static IServiceCollection AddRepositoriesAndMediatR(this IServiceCollection services)
        {
            return
                services
                    .AddScoped(typeof(IUnitOfWorkRepository<>), typeof(UnitOfWorkRepository<>))
                    .AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>))
                    .AddScoped<IIdentityUserRepository, IdentityUserRepository>()
                    .AddScoped<IIdentityRoleRepository, IdentityRoleRepository>()
                    .AddScoped<ISeedUserClaimRepository, SeedUserClaimRepository>()
                    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
