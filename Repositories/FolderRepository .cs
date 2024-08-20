using DirectoryPermissionManagement.Models;

namespace DirectoryPermissionManagement.Repositories
{
    public class FolderRepository
    {
        private readonly ApplicationContext _context;

        public FolderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Folder? GetById(int id)
        {
            return _context.Folders.Find(id);
        }

        public Folder Insert(Folder folder)
        {
            _context.Folders.Add(folder);
            _context.SaveChanges();
            return folder;
        }

        public Folder Update(Folder folder)
        {
            _context.ChangeTracker.Clear();
            _context.Folders.Update(folder);
            _context.SaveChanges();
            return folder;
        }

        public void Delete(Folder folder) 
        {
            _context.Folders.Remove(folder);
            _context.SaveChanges();
        }

        public bool IsExisted(int id)
        {
            var folderInDb = _context.Folders.Find(id);
            return folderInDb != null;
        }

        public bool HadNameAndDriveIdAndParrentFolderId(string name, int driveId, int? parrentFolderId)
        {
            var folderInDb = _context.Folders.SingleOrDefault(f => f.Name == name && f.DriveId == driveId && f.ParrentFolderId == parrentFolderId);
            return folderInDb != null;
        }

    }
}
