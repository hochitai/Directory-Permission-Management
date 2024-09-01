using DirectoryPermissionManagement.Commons;
using DirectoryPermissionManagement.Configs;
using DirectoryPermissionManagement.Models;
using DirectoryPermissionManagement.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DirectoryPermissionManagement.Services
{
    public class DriveService
    {
        private readonly DriveRepository _driveRepository;
        private readonly PermissionRepository _permissionRepository;

        public DriveService(DriveRepository driveRepository, PermissionRepository permissionRepository)
        {
            _driveRepository = driveRepository;
            _permissionRepository = permissionRepository;
        }
        /*
        public async Task<List<Folder>?> GetFoldersById(int id, int userId)
        {
            if (! await _driveRepository.IsOwner(id, userId))
            {
                return null;
            }
            var result = await _driveRepository.GetFoldersById(id);
            return result;
        }

        public async Task<List<Item>?> GetFilesById(int id, int userId)
        {
            if (! await _driveRepository.IsOwner(id, userId))
            {
                return null;
            }
            var result = await _driveRepository.GetFilesById(id);
            return result;
        }
        */
        public async Task<List<Drive>?> GetByUserId(int userId)
        {
            var result = await _driveRepository.GetByUserId(userId);
            return result;
        }

        public async Task<bool> HasNameAndUserId(string driveName, int userId)
        {
            return await _driveRepository.HasNameAndUserId(driveName, userId);
        }


        public async Task<Drive?> Insert(Drive drive, int userId)
        {
            drive.UserId = userId;

            var result = await _driveRepository.Insert(drive);
            if (result == null)
            {
                return null;
            }

            await _permissionRepository.GrantPermission(userId, result.Id, null, null, (int) RoleEnum.Admin);

            return result;
        }

        public async Task<Drive?> Update(int id, Drive drive, int userId)
        {
            drive.Id = id;
            // Add owner
            drive.UserId = userId;

            var result = await _driveRepository.Update(drive);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _driveRepository.GetById(id);
            if (result == null)
            {
                return false;
            }

            await _driveRepository.Delete(result);
            return true;
        }
    }
}
