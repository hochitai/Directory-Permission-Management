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

        public Item? GetById(int id)
        {
            var result = _itemRepository.GetById(id);
            return result;
        }

        public Item? Insert(Item item)
        {
            if (_itemRepository.HadNameAndDriveIdAndFolderId(item.Name, item.DriveId, (int?)item.FolderId))
            {
                return null;
            }
            var result = _itemRepository.Insert(item);
            return result;
        }

        public Item? Update(int id, Item item)
        {
            if (!_itemRepository.IsExisted(id))
            {
                return null;
            }
            item.Id = id;
            var result = _itemRepository.Update(item);
            return result;
        }

        public bool Delete(int id)
        {
            var result = _itemRepository.GetById(id);
            if (result == null)
            {
                return false;
            }

            _itemRepository.Delete(result);
            return true;
        }
    }
}
