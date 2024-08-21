using DirectoryPermissionManagement.Models;

namespace DirectoryPermissionManagement.Repositories
{
    public class PermissionRepository
    {
        private readonly ApplicationContext _context;

        public PermissionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Permission>? GetByUserId(int userId)
        {
            return _context.Permissions.Where(p => p.UserId == userId).ToList();
        }

        public Permission Insert(Permission permission)
        {
            _context.Permissions.Add(permission);
            _context.SaveChanges();
            return permission;
        }

        public Permission Update(Permission permission)
        {
            _context.ChangeTracker.Clear();
            _context.Permissions.Update(permission);
            _context.SaveChanges();
            return permission;
        }

        public void Delete(Permission permission) 
        {
            _context.Permissions.Remove(permission);
            _context.SaveChanges();
        }

        public bool IsExisted(Permission permission)
        {
            var permissionInDb = _context.Permissions.SingleOrDefault(p => p.UserId == permission.UserId && p.ItemId == permission.ItemId 
            && p.FolderId == permission.FolderId);
            return permissionInDb != null;
        }

    }
}
