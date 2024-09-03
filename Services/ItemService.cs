using DirectoryPermissionManagement.Configs;
using DirectoryPermissionManagement.Models;
using DirectoryPermissionManagement.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DirectoryPermissionManagement.Services
{
    public class ItemService
    {
        private readonly ItemRepository _itemRepository;
        private readonly PermissionRepository _permissionRepository;

        public ItemService(ItemRepository itemRepository, PermissionRepository permissionRepository)
        {
            _itemRepository = itemRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<Item?> GetById(int id)
        {
            var result = await _itemRepository.GetById(id);
            return result;
        }

        public async Task<List<Item>?> GetFilesByFolderId(int folderId)
        {
            var result = await _itemRepository.GetFilesByFolderId(folderId);
            return result;
        }

        public async Task<Item?> Insert(Item item)
        {
            if (await _itemRepository.HadNameAndDriveIdAndFolderId(item.Name, item.DriveId, (int?)item.FolderId))
            {
                return null;
            }

            var result = await _itemRepository.Insert(item);
            var users = new List<Permission> { };

            if (item.FolderId == null || item.FolderId == 0)
            {
                users = await _permissionRepository.GetUserIdHavePermissionByDriveId(result.DriveId);
            }
            else
            {
                users = await _permissionRepository.GetUserIdHavePermissionByFolderId(result.FolderId);
            }

            foreach (var user in users)
            {
                await _permissionRepository.GrantPermission(user.UserId, null, result.Id, null, user.RoleId);
            }

            return result;
        }

        public async Task<Item?> Update(int id, Item item)
        {
            if (! await _itemRepository.IsExisted(id))
            {
                return null;
            }
            item.Id = id;
            var result = await _itemRepository.Update(item);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var userSubs = await _permissionRepository.GetUserIdHavePermissionByItemId(id);
            foreach (var user in userSubs)
            {
                await _permissionRepository.DeletePermission(user.UserId, null, null, id, user.RoleId);
            }

            var result = await _itemRepository.GetById(id);
            if (result == null)
            {
                return false;
            }

            await _itemRepository.Delete(result);
            return true;
        }
    }
}
