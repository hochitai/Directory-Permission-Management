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

        public async Task<List<Item>?> GetFilesByDriveId(int driveId)
        {
            return await _context.Items.Where(i => i.DriveId == driveId && i.FolderId == 0).ToListAsync();
        }

        public async Task<List<Item>?> GetFilesByFolderId(int folderId)
        {
            var results = from i in _context.Items
                          where i.FolderId == folderId
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
            var itemInDb = await _context.Items.FirstOrDefaultAsync(i => i.Name == name && i.DriveId == driveId && i.FolderId == folderId);
            return itemInDb != null;
        }

    }
}
