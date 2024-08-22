using DirectoryPermissionManagement.Configs;
using DirectoryPermissionManagement.Models;
using DirectoryPermissionManagement.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DirectoryPermissionManagement.Services
{
    public class FolderService
    {
        private readonly FolderRepository _folderRepository;
        private readonly PermissionRepository _permissionRepository;

        public FolderService(FolderRepository folderRepository, PermissionRepository permissionRepository)
        {
            _folderRepository = folderRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<List<Folder>?> GetSubFoldersById(int id, int userId)
        {
            var result = await _folderRepository.GetSubFoldersById(id, userId);
            return result;
        }

        public async Task<List<Item>?> GetFilesById(int id, int userId)
        {
            var result = await _folderRepository.GetFilesById(id, userId);
            return result;
        }

        public async Task<Folder?> Insert(Folder folder)
        {
            if (await _folderRepository.HadNameAndDriveIdAndParrentFolderId(folder.Name, folder.DriveId, (int?)folder.ParrentFolderId))
            {
                return null;
            }
            var result = await _folderRepository.Insert(folder);

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
            var result = await _folderRepository.GetById(id);
            if (result == null)
            {
                return false;
            }

            await _folderRepository.Delete(result);
            return true;
        }
    }
}
