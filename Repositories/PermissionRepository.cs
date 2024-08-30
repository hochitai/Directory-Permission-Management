using DirectoryPermissionManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DirectoryPermissionManagement.Repositories
{
    public class PermissionRepository
    {
        private readonly ApplicationContext _context;

        public PermissionRepository(ApplicationContext context)
        {
            _context = context;
        }

        /* public async Task<Permission> Update(Permission permission)
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
        }*/

        public async Task<int> GetPermissionRole(int userId, int? driveId, int? folderId, int? fileId)
        {
            return await (from p in _context.Permissions
                    join r in _context.Roles on p.RoleId equals r.Id
                    where p.UserId == userId && p.DriveId == driveId && p.FolderId == folderId && p.ItemId == fileId
                    select r.Id).FirstAsync();
        }

        public async Task GrantPermission(int userId, int? driveId, int? folderId, int? fileId, int roleId)
        {
            var permission = new Permission
            {
                UserId = userId,
                RoleId = roleId,
                DriveId = driveId,
                FolderId = folderId,
                ItemId = fileId
            };
            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();
        }

    }
}
