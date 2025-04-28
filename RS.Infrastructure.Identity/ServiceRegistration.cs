using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RS.Core.Application.Interfaces.Services;
using RS.Infrastructure.Identity.Contexts;
using RS.Infrastructure.Identity.Entities;
using RS.Infrastructure.Identity.Seeds;
using RS.Infrastructure.Identity.Service;

namespace RS.Infrastructure.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            #region Contexts

            var connectionString = configuration.GetConnectionString("IdentityConnection");
            services.AddDbContext<IdentityContext>(options =>
            options.UseSqlServer(connectionString, m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));

            #endregion

            #region Identity

            services.AddIdentityCore<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddSignInManager()
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromSeconds(300);
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            }).AddCookie(IdentityConstants.ApplicationScheme, opt =>
            {
                opt.ExpireTimeSpan = TimeSpan.FromHours(12);
                opt.LoginPath = "/User";
                opt.AccessDeniedPath = "/User/AccessDenied";
            });
            #endregion

            #region Services
            services.AddTransient<IAccountService, AccountService>();
            #endregion

        }

        public static async Task RunIdentitySeeds(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    await DefaultRoles.SeedAsync(roleManager);
                    await DefaultSuperAdminUser.SeedAsync(userManager);
                    await DefaultBasicUser.SeedAsync(userManager);
                }
                catch (Exception ex) {}
            }
        }
    }
}
