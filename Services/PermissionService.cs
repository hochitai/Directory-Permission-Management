using DirectoryPermissionManagement.Configs;
using DirectoryPermissionManagement.Models;
using DirectoryPermissionManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using DirectoryPermissionManagement.Commons;

namespace DirectoryPermissionManagement.Services
{
    public class PermissionService
    {
        private readonly PermissionRepository _permissionRepository;
        private readonly DriveRepository _driveRepository;
        private readonly FolderRepository _folderRepository;
        private readonly ItemRepository _itemRepository;

        public PermissionService(PermissionRepository permissionRepository, FolderRepository folderRepository, ItemRepository itemRepository)
        {
            _permissionRepository = permissionRepository;
            _folderRepository = folderRepository;
            _itemRepository = itemRepository;
        }
        /*
        public async Task<List<Permission>?> GetByUserId(int userId)
        {
            var result = await _permissionRepository.GetByUserId(userId);
            return result;
        }

        public async Task<Permission?> Insert(Permission permission)
        {
            if (await _permissionRepository.IsExisted(permission))
            {
                return null;
            }
            var result = await _permissionRepository.Insert(permission);
            return result;
        }

        public async Task<Permission?> Update(Permission permission)
        {
            if (! await _permissionRepository.IsExisted(permission))
            {
                return null;
            }
            var result = await _permissionRepository.Update(permission);
            return result;
        }

        public async Task<bool> Delete(Permission permission)
        {
            var role = await _permissionRepository.GetPermissionRole(
                permission.UserId, (int?)permission.DriveId, permission.FolderId, permission.ItemId);
            await _permissionRepository.Delete(permission);
            return true;
        } */

        public async Task<bool> HasPermission(int userId, int? driveId, int? folderId, int? fileId, int roleId)
        {
            var roleIdInDb = await _permissionRepository.GetPermissionRole(userId, driveId, folderId, fileId);
            return roleIdInDb == roleId;
        }

        public async Task GrantFolderPermission(int userId, int folderId, int roleId)
        {
            await _permissionRepository.GrantPermission(userId, null, folderId, null, roleId);
            await GrantSubFolderAndFile(folderId, userId, roleId);
        }

        public async Task GrantFilePermission(int userId, int fileId, int roleId)
        {
            await _permissionRepository.GrantPermission(userId, null, null, fileId, roleId);
        }

        public async Task GrantDrivePermission(int userId, int driveId, int roleId)
        {
            await _permissionRepository.GrantPermission(userId, driveId, null, null, roleId);
            var folders = await _folderRepository.GetFoldersByDriveId(driveId);
            var files = await _itemRepository.GetFilesByDriveId(driveId );

            foreach  (var file in files)
            {
                await _permissionRepository.GrantPermission(userId, null, null, file.Id, roleId);
            }

            foreach (var folder in folders)
            {
                await GrantSubFolderAndFile(folder.Id, userId, roleId);
            } 
        }

        public async Task GrantSubFolderAndFile(int folderId, int userId, int roleId)
        {
            var subFolders = await _folderRepository.GetSubFoldersById(folderId);
            var subFiles = await _itemRepository.GetFilesByFolderId(folderId);

            foreach (var file in subFiles)
            {
                await _permissionRepository.GrantPermission(userId, null, null, file.Id, roleId);
            }

            foreach (var folder in subFolders)
            {
                await _permissionRepository.GrantPermission(userId, null, folder.Id, null, roleId);
                await GrantSubFolderAndFile(folder.Id, userId, roleId);
            }
        }
    }

}
