using DirectoryPermissionManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.RegularExpressions;

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
            string sql = "INSERT INTO Permissions (UserId, DriveId,FolderId, ItemID, RoleID) VALUES (@p0, @p1, @p2, @p3, @p4)";
            await _context.Database.ExecuteSqlRawAsync(sql, userId, driveId, folderId, fileId, roleId);
        }

        public async Task DeletePermission(int userId, int? driveId, int? folderId, int? fileId, int roleId)
        {
            string sql = "DELETE FROM Permissions WHERE UserId = @p0 AND RoleId = @p1";

            if (driveId != null)
            {
                sql += " AND DriveId = @p2 AND (FolderId = @p3 OR @p3 IS NULL) AND (ItemId = @p4 OR @p4 IS NULL)";
            }

            if (folderId != null) 
            {
                sql += " AND (DriveId = @p2 OR @p2 IS NULL) AND FolderId = @p3 AND (ItemId = @p4 OR @p4 IS NULL) ";
            }

            if (fileId != null) 
            {
                sql += " AND (DriveId = @p2 OR @p2 IS NULL) AND (FolderId = @p3 OR @p3 IS NULL) AND ItemId = @p4";
            }

            await _context.Database.ExecuteSqlRawAsync(sql, userId, roleId, driveId, folderId, fileId);
        }

    }
}
