using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.Common.Interfaces;
using Finance.Infrastructure.Identity;
using Finance.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FinanceIdentityDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Identity")));

            services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<FinanceIdentityDbContext>()
                .AddSignInManager<SignInManager<IdentityUser>>();

            services.AddScoped<IUserManager, UserManager>();

            return services;
        }
    }
}
