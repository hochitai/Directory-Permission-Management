using DirectoryPermissionManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace DirectoryPermissionManagement.Repositories
{
    public class ItemRepository
    {
        private readonly ApplicationContext _context;

        public ItemRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Item?> GetById(int id)
        {
            return await _context.Items.FindAsync(id);
        }
        public async Task<List<Item>?> GetFilesById(int id, int userId)
        {
            var results = from i in _context.Items
                          join d in _context.Drives on i.DriveId equals d.Id
                          where d.UserId == userId && i.FolderId == id
                          select i;
            return await results.ToListAsync();
        }

        public async Task<Item> Insert(Item item)
        {
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Item> Update(Item item)
        {
            _context.ChangeTracker.Clear();
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task Delete(Item item) 
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExisted(int id)
        {
            var itemInDb = await _context.Items.FindAsync(id);
            return itemInDb != null;
        }

        public async Task<bool> HadNameAndDriveIdAndFolderId(string name, int driveId, int? folderId)
        {
            var itemInDb = await _context.Items.SingleOrDefaultAsync(i => i.Name == name && i.DriveId == driveId && i.FolderId == folderId);
            return itemInDb != null;
        }

    }
}
