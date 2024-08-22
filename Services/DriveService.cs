using DirectoryPermissionManagement.Configs;
using DirectoryPermissionManagement.Models;
using DirectoryPermissionManagement.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DirectoryPermissionManagement.Services
{
    public class DriveService
    {
        private readonly DriveRepository _driveRepository;

        public DriveService(DriveRepository driveRepository)
        {
            _driveRepository = driveRepository;
        }

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

        public async Task<List<Drive>?> GetByUserId(int userId)
        {
            var result = await _driveRepository.GetByUserId(userId);
            return result;
        }

        public async Task<Drive?> Insert(Drive drive, int userId)
        {
            if (await _driveRepository.HadNameAndUserId(drive.Name, userId))
            {
                return null;
            }
            drive.UserId = userId;

            var result = await _driveRepository.Insert(drive);
            return result;
        }

        public async Task<Drive?> Update(int id, Drive drive, int userId)
        {
            if ( ! await _driveRepository.IsExisted(id))
            {
                return null;
            }
            
            drive.Id = id;
            drive.UserId = userId;

            var result = await _driveRepository.Update(drive);
            return result;
        }

        public async Task Delete(int id, int userId)
        {
            var result = await _driveRepository.GetById(id, userId);
            if (result == null)
            {
                return;
            }

            await _driveRepository.Delete(result);
        }
    }
}
