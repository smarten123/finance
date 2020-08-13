using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.Common.Interfaces;

namespace Finance.Web.FunctionalTests.Common
{
    public class DataSeeding
    {
        public static void SeedUserAccounts(IUserManager userManager)
        {
            userManager.CreateAsync("stanford@gmail.com", "P@ssw0rd").Wait();
            userManager.CreateAsync("oxford@gmail.com", "S3cr3t)").Wait();
        }
    }
}
