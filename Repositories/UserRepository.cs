using DirectoryPermissionManagement.DTOs;
using DirectoryPermissionManagement.Models;

namespace DirectoryPermissionManagement.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public User Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User Login(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool IsExisted(string username) {
            var userInDb = _context.Users.SingleOrDefault(u => u.Username == username);
            return userInDb != null;
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.SingleOrDefault(u => u.Username == username);
        }

    }
}
