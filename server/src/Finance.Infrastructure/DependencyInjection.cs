using Finance.Application.Common.Interfaces;
using Finance.Infrastructure.Identity;
using Finance.Infrastructure.Identity.Services;
using Finance.Infrastructure.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Finance.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FinanceIdentityDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Identity")));

            services.ConfigureLogging();

            services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<FinanceIdentityDbContext>()
                .AddSignInManager<SignInManager<IdentityUser>>();

            services.AddScoped<IUserManager, UserManager>();

            return services;
        }

        private static IServiceCollection ConfigureLogging(this IServiceCollection services)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            StaticLogger.Logger = new SerilogLogAdapter(logger);
            services.AddSingleton<IAppLogger, SerilogLogAdapter>(sp => new SerilogLogAdapter(logger));

            return services;
        }
    }
}
