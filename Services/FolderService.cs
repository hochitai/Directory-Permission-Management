using DirectoryPermissionManagement.Configs;
using DirectoryPermissionManagement.Models;
using DirectoryPermissionManagement.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DirectoryPermissionManagement.Services
{
    public class FolderService
    {
        private readonly FolderRepository _folderRepository;
        private readonly ItemRepository _itemRepository;
        private readonly PermissionRepository _permissionRepository;

        public FolderService(FolderRepository folderRepository, ItemRepository itemRepository, PermissionRepository permissionRepository)
        {
            _folderRepository = folderRepository;
            _itemRepository = itemRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<List<Folder>?> GetSubFoldersById(int folderId)
        {
            var result = await _folderRepository.GetSubFoldersById(folderId);
            return result;
        }

        public async Task<Folder?> Insert(Folder folder)
        {
            if (await _folderRepository.HadNameAndDriveIdAndParrentFolderId(folder.Name, folder.DriveId, (int?)folder.ParrentFolderId))
            {
                return null;
            }

            var result = await _folderRepository.Insert(folder);
            var users = new List<Permission> {};

            if (folder.ParrentFolderId == null || folder.ParrentFolderId == 0)
            {
                users = await _permissionRepository.GetUserIdHavePermissionByDriveId(result.DriveId);
            }
            else
            {
                users = await _permissionRepository.GetUserIdHavePermissionByFolderId((int)result.ParrentFolderId);
            }

            foreach (var user in users) 
            {
                await _permissionRepository.GrantPermission(user.UserId, null, result.Id, null, user.RoleId);
            }

            return result;
        }

        public async Task<Folder?> Update(int id, Folder folder)
        {
            if (! await _folderRepository.IsExisted(id))
            {
                return null;
            }
            folder.Id = id;
            var result = await _folderRepository.Update(folder);
            return result;
        }

        public async Task<bool> Delete(int id)
        {

            await DeleteSubFolderAndFile(id);

            var result = await _folderRepository.GetById(id);
            if (result == null)
            {
                return false;
            }
           
            await _folderRepository.Delete(result);
            return true;
        }

        public async Task DeleteSubFolderAndFile(int folderId)
        {
            var subFolders = await _folderRepository.GetSubFoldersById(folderId);
            var subFiles = await _itemRepository.GetFilesByFolderId(folderId);

            foreach (var file in subFiles)
            {
                var userSubs = await _permissionRepository.GetUserIdHavePermissionByItemId(file.Id);
                foreach (var user in userSubs)
                {
                    await _permissionRepository.DeletePermission(user.UserId, null, null, file.Id, user.RoleId);
                }
                await _itemRepository.Delete(file);
            }

            foreach (var folder in subFolders)
            {
                await DeleteSubFolderAndFile(folder.Id);
                var userSubs = await _permissionRepository.GetUserIdHavePermissionByFolderId(folder.Id);
                foreach (var user in userSubs)
                {
                    await _permissionRepository.DeletePermission(user.UserId, null, folder.Id, null, user.RoleId);
                }
                await _folderRepository.Delete(folder);
            }
        }
    }
}
