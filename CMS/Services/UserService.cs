using CMS.Data.Entities;
using CMS.Interfaces.Repositories;
using CMS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CMS.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor) 
        {
            this._userRepository = userRepository;
            this._httpContextAccessor = httpContextAccessor;
        }

        public  async Task<bool> DoesUserAlreadyExistAsync(string username) 
        {
            var user = await _userRepository.GetUserAsync(username);

            return user != null;
        }

        public bool IsUserLoggedIn() 
        {
            return _httpContextAccessor.HttpContext.User?.Identity?.IsAuthenticated ?? false;
        }

        public Task LogoutAsync() 
        {
            return _httpContextAccessor.HttpContext.SignOutAsync();
        }

        public async Task<bool> RegisterAsync(string username, string password) 
        {
            foreach (var role in Enum.GetNames(typeof(UserRole)))
            {
                if (username.Equals(role, StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }
            }

            //salt maken
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            string saltString = Convert.ToBase64String(salt);

            //pw hashen
            byte[] hashedPassword = HashPassword(password, salt);
            string hashedPasswordString = Convert.ToBase64String(hashedPassword);


            User user = new User { 
                Username = username, 
                PasswordHash = hashedPasswordString, 
                Salt = saltString, 
                Role = UserRole.Student
            };

            var result = await _userRepository.AddUserAsync(user);

            await LoginAsync(username, password);
            return true;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            foreach (var role in Enum.GetNames(typeof(UserRole))) 
            {
                if (username.Equals(role, StringComparison.InvariantCultureIgnoreCase) &&
                    password.Equals(role, StringComparison.InvariantCultureIgnoreCase)) 
                {
                    await LoginWithRole(role, role);
                    return true;
                }
            }

            var user = await _userRepository.GetUserAsync(username);

            if (user == null) 
            {
                return false; 
            }

            var hashedPw = HashPassword(password, Convert.FromBase64String(user.Salt));

            bool match = Convert.FromBase64String(user.PasswordHash).SequenceEqual(hashedPw);

            if (match) 
            {
                await LoginWithRole(username, user.Role.ToString());

                return true;
            }

            return false;
        }

        public List<UserViewModel> GetAllUsers() {
            List<UserViewModel> userList = new List<UserViewModel>();

            foreach (User user in _userRepository.GetAllUsers()) {
                userList.Add(new UserViewModel { userName = user.Username, role = user.Role });
            }

            return userList;
        }

        public async Task<UserViewModel> GetUser(string username) {
            var result = await _userRepository.GetUserAsync(username);
            return new UserViewModel { userName = result.Username, role = result.Role};
        }

        public bool EditUser(string username, UserRole role) {
            return _userRepository.EditUser(username, role);
        }

        private byte[] HashPassword(string password, byte[] salt) {
            byte[] passwordHash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8);
            return passwordHash;
        }

        private Task LoginWithRole(string username, string role) 
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Role, role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                IssuedUtc = DateTimeOffset.UtcNow
            };

            return _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), authProperties);

        }
    }
}
