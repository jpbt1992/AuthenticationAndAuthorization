using AuthenticationAndAuthorization.Infrastructure.Extensions;
using AuthenticationAndAuthorization.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace AuthenticationAndAuthorization.WebAPI
{
    public sealed class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddIdentity();

            builder.Services.AddControllers(configure =>
            {
                var policy = new AuthorizationPolicyBuilder()
                       .RequireAuthenticatedUser()
                       .Build();

                configure.Filters.Add(new AuthorizeFilter(policy));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services
                .Configure<CookiePolicyOptions>(options =>
                {
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                })
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/api/Account/Login";
                    options.AccessDeniedPath = "/api/Account/AccessDenied";
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(3);
                    options.SlidingExpiration = true;
                });

            builder.Services
                .AddDbContexts(builder.Configuration)
                .AddRepositoriesAndMediatR();

            var app = builder.Build();

            #region CreateUsersClaims
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

            if (scopedFactory != null)
            {
                using var scope = scopedFactory.CreateScope();

                var service = scope.ServiceProvider.GetService<ISeedUserClaimRepository>();

                if (service != null)
                {
                    await service.SeedUsersClaims();
                }
            }
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
