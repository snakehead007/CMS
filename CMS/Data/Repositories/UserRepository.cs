using CMS.Data.Entities;
using CMS.Interfaces.Repositories;
using System.Threading.Tasks;

namespace CMS.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext) {
            this._dataContext = dataContext;
        }

        public async Task<User> AddUserAsync(User user)
        {
            var entity = await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return entity.Entity;
        }

        public Task<User> GetUserAsync(string username)
        {
            return _dataContext.Users.FindAsync(username).AsTask();
        }
    }
}
