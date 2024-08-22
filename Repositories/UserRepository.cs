using DirectoryPermissionManagement.DTOs;
using DirectoryPermissionManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace DirectoryPermissionManagement.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> IsExisted(string username) {
            var userInDb = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            return userInDb != null;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

    }
}
