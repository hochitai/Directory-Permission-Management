using DirectoryPermissionManagement.Models;

namespace DirectoryPermissionManagement.Repositories
{
    public class ItemRepository
    {
        private readonly ApplicationContext _context;

        public ItemRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Item? GetById(int id)
        {
            return _context.Items.Find(id);
        }

        public Item Insert(Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Item Update(Item item)
        {
            _context.ChangeTracker.Clear();
            _context.Items.Update(item);
            _context.SaveChanges();
            return item;
        }

        public void Delete(Item item) 
        {
            _context.Items.Remove(item);
            _context.SaveChanges();
        }

        public bool IsExisted(int id)
        {
            var itemInDb = _context.Items.Find(id);
            return itemInDb != null;
        }

        public bool HadNameAndDriveIdAndFolderId(string name, int driveId, int? folderId)
        {
            var itemInDb = _context.Items.SingleOrDefault(i => i.Name == name && i.DriveId == driveId && i.FolderId == folderId);
            return itemInDb != null;
        }

    }
}
