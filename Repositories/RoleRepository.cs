using DirectoryPermissionManagement.Models;

namespace DirectoryPermissionManagement.Repositories
{
    public class RoleRepository
    {
        private readonly ApplicationContext _context;

        public RoleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Role? GetById(int id)
        {
            return _context.Roles.FirstOrDefault(x => x.Id == id);
        }

        public Role Insert(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }

        public Role Update(Role role)
        {
            _context.ChangeTracker.Clear();
            _context.Roles.Update(role);
            _context.SaveChanges();
            return role;
        }

        public void Delete(Role role) 
        {
            _context.Roles.Remove(role);
            _context.SaveChanges();
        }

        public bool IsExisted(int id)
        {
            var roleInDb = _context.Roles.Find(id);
            return roleInDb != null;
        }

        public bool HadName(string name)
        {
            var roleInDb = _context.Roles.SingleOrDefault(r => r.Name == name);
            return roleInDb != null;
        }

    }
}
