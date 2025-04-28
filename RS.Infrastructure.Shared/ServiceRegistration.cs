using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RS.Core.Application.Interfaces.Services;
using RS.Core.Domain.Settings;
using RS.Infrastructure.Shared.Services;

namespace RS.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
