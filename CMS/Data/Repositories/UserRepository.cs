using CMS.Data.Entities;
using CMS.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

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
            return GetUser(username);
        }

        public bool EditUser(string username, UserRole role)
        {
            User user = GetUser(username).Result;
            user.Role = role;
            try
            {
                _dataContext.SaveChanges();
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        public List<User> GetAllUsers()
        {
            return _dataContext.Users.ToList();
        }

        public async Task<User> GetUser(string username) {
            return await _dataContext.Users.FindAsync(username);
        }
    }
}
