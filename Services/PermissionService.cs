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

        public List<Permission>? GetByUserId(int userId)
        {
            var result = _permissionRepository.GetByUserId(userId);
            return result;
        }

        public Permission? Insert(Permission permission)
        {
            if (_permissionRepository.IsExisted(permission))
            {
                return null;
            }
            var result = _permissionRepository.Insert(permission);
            return result;
        }

        public Permission? Update(Permission permission)
        {
            if (!_permissionRepository.IsExisted(permission))
            {
                return null;
            }
            var result = _permissionRepository.Update(permission);
            return result;
        }

        public bool Delete(Permission permission)
        {
            _permissionRepository.Delete(permission);
            return true;
        }
    }
}
