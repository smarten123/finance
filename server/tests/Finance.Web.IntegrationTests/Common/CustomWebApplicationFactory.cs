﻿using System;
using System.Linq;
using Finance.Application.Common.Interfaces;
using Finance.Infrastructure.Identity;
using Finance.Web.FunctionalTests.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Web.FunctionalTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private bool _disposed;
        private readonly SqliteConnection _sqliteConn;

        public CustomWebApplicationFactory()
        {
            _disposed = false;
            
            _sqliteConn = new SqliteConnection("Data source=:memory:");
            _sqliteConn.Open();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                var dbContextOptionsDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<FinanceIdentityDbContext>));
                services.Remove(dbContextOptionsDescriptor);

                services.AddDbContext<FinanceIdentityDbContext>(options =>
                {
                    options.UseSqlite(_sqliteConn);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<FinanceIdentityDbContext>();

                    context.Database.EnsureCreated();
                }

                var userManager = sp.GetRequiredService<IUserManager>();
                DataSeeding.SeedUserAccounts(userManager);
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _sqliteConn.Close();
            }

            _disposed = true;

            base.Dispose(disposing);
        }
    }
}
