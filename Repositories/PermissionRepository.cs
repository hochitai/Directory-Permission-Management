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

        public async Task<List<Permission>> GetUserIdHavePermissionByDriveId(int driveId)
        {
            return await (from p in _context.Permissions
                          where p.DriveId == driveId
                          select new Permission{ UserId = p.UserId, RoleId = p.RoleId}).ToListAsync();
        }

        public async Task<List<Permission>> GetUserIdHavePermissionByFolderId(int folderId)
        {
            return await (from p in _context.Permissions
                          where p.FolderId == folderId
                          select new Permission { UserId = p.UserId, RoleId = p.RoleId }).ToListAsync();
        }
        public async Task<List<Permission>> GetUserIdHavePermissionByItemId(int itemId)
        {
            return await (from p in _context.Permissions
                          where p.ItemId == itemId
                          select new Permission { UserId = p.UserId, RoleId = p.RoleId }).ToListAsync();
        }

        public async Task<int> GetPermissionRole(int userId, int? driveId, int? folderId, int? fileId)
        {
            return await (from p in _context.Permissions
                    where p.UserId == userId && p.DriveId == driveId && p.FolderId == folderId && p.ItemId == fileId
                    select p.RoleId).FirstOrDefaultAsync();
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

        public async Task DeletePermission(int userId, int? driveId, int? folderId, int? fileId, int roleId)
        {
            var permission = new Permission
            {
                UserId = userId,
                RoleId = roleId,
                DriveId = driveId,
                FolderId = folderId,
                ItemId = fileId
            };
            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
        }

    }
}
