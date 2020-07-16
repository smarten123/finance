using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Identity
{
    public class FinanceIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public FinanceIdentityDbContext(DbContextOptions<FinanceIdentityDbContext> options) : base(options)
        {
        }
    }
}
