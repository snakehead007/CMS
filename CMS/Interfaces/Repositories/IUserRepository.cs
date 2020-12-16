using CMS.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Interfaces.Repositories
{
   public interface IUserRepository
    {
        Task<User> GetUserAsync(string username);
        Task<User> AddUserAsync(User user);
        List<User> GetAllUsers();
        bool EditUser(string username, UserRole role);
    }
}
