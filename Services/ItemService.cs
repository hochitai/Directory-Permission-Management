using DirectoryPermissionManagement.Configs;
using DirectoryPermissionManagement.Models;
using DirectoryPermissionManagement.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DirectoryPermissionManagement.Services
{
    public class ItemService
    {
        private readonly ItemRepository _itemRepository;

        public ItemService(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<Item?> GetById(int id)
        {
            var result = await _itemRepository.GetById(id);
            return result;
        }

        public async Task<Item?> Insert(Item item)
        {
            if (await _itemRepository.HadNameAndDriveIdAndFolderId(item.Name, item.DriveId, (int?)item.FolderId))
            {
                return null;
            }
            var result = await _itemRepository.Insert(item);
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
