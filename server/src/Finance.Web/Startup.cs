using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.Auth;
using Finance.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Finance.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomSwagger();

            services.AddCustomIdentity(Configuration)
                .AddCustomAuthentication(Configuration);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Finance Web API v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Finance",
                    Version = "v1"
                });
            });

            return services;
        }

        public static IServiceCollection AddCustomIdentity(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<TokenProviderOptions>(options =>
            {
                options.Audience = configuration.GetValue<string>("Token:Audience");
                options.Issuer = configuration.GetValue<string>("Token:Issuer");
                options.ExpiresAt = TimeSpan.FromSeconds(300);

                var secret = configuration.GetValue<string>("Token:Secret");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
                options.SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            });

            services.AddDbContext<FinanceIdentityDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Identity")));

            services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<FinanceIdentityDbContext>()
                .AddSignInManager<SignInManager<IdentityUser>>();

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var secret = configuration.GetValue<string>("Token:Secret");
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration.GetValue<string>("Token:Issuer"),
                        ValidAudience = configuration.GetValue<string>("Token:Audience"),
                        IssuerSigningKey = key
                    };
                });

            return services;
        }
    }
}
