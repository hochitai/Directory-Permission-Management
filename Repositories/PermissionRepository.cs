using DirectoryPermissionManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace DirectoryPermissionManagement.Repositories
{
    public class PermissionRepository
    {
        private readonly ApplicationContext _context;

        public PermissionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Permission>?> GetByUserId(int userId)
        {
            return await _context.Permissions.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<Permission> Insert(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task<Permission> Update(Permission permission)
        {
            _context.ChangeTracker.Clear();
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task Delete(Permission permission) 
        {
            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExisted(Permission permission)
        {
            var permissionInDb = await _context.Permissions.SingleOrDefaultAsync(p => p.UserId == permission.UserId && p.ItemId == permission.ItemId 
            && p.FolderId == permission.FolderId);
            return permissionInDb != null;
        }

    }
}
