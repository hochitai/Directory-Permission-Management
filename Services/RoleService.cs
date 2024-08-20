using DirectoryPermissionManagement.Configs;
using DirectoryPermissionManagement.Models;
using DirectoryPermissionManagement.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DirectoryPermissionManagement.Services
{
    public class RoleService
    {
        private readonly RoleRepository _roleRepository;

        public RoleService(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Role? GetById(int id)
        {
            var result = _roleRepository.GetById(id);
            return result;
        }

        public Role? Insert(Role role)
        {
            if (_roleRepository.HadName(role.Name))
            {
                return null;
            }
            var result = _roleRepository.Insert(role);
            return result;
        }

        public Role? Update(int id, Role role)
        {
            if (!_roleRepository.IsExisted(id))
            {
                return null;
            }
            role.Id = id;
            var result = _roleRepository.Update(role);
            return result;
        }

        public bool Delete(int id)
        {
            var result = _roleRepository.GetById(id);
            if (result == null)
            {
                return false;
            }

            _roleRepository.Delete(result);
            return true;
        }
    }
}
