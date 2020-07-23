using System.Threading.Tasks;
using Finance.Application.Common.Models;

namespace Finance.Application.Common.Interfaces
{
    public interface IUserManager
    {
        Task<Result> CreateAsync(string email, string password);
    }
}
