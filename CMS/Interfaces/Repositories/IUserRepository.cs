using CMS.Data.Entities;
using System.Threading.Tasks;

namespace CMS.Interfaces.Repositories
{
   public interface IUserRepository
    {
        Task<User> GetUserAsync(string username);
        Task<User> AddUserAsync(User user);
    }
}
