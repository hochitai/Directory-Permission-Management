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

        public Drive? GetById(int id)
        {
            var result = _driveRepository.GetById(id);
            return result;
        }

        public List<Drive>? GetByUserId(int userId)
        {
            var result = _driveRepository.GetByUserId(userId);
            return result;
        }

        public Drive? Insert(Drive drive)
        {
            if (_driveRepository.IsExisted(drive))
            {
                return null;
            }
            var result = _driveRepository.Insert(drive);
            return result;
        }

        public Drive? Update(int id, Drive drive)
        {
            if (!_driveRepository.IsExisted(id))
            {
                return null;
            }
            drive.Id = id;
            var result = _driveRepository.Update(drive);
            return result;
        }

        public void Delete(int id)
        {
            var result = _driveRepository.GetById(id);
            if (result == null)
            {
                return;
            }

            _driveRepository.Delete(result);
        }
    }
}
