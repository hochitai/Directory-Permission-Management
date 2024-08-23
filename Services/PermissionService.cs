using DirectoryPermissionManagement.Configs;
using DirectoryPermissionManagement.Models;
using DirectoryPermissionManagement.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DirectoryPermissionManagement.Services
{
    public class PermissionService
    {
        private readonly PermissionRepository _permissionRepository;

        public PermissionService(PermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

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
            await _permissionRepository.Delete(permission);
            return true;
        }
    }
}
