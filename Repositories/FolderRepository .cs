using DirectoryPermissionManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace DirectoryPermissionManagement.Repositories
{
    public class FolderRepository
    {
        private readonly ApplicationContext _context;

        public FolderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Folder?> GetById(int id)
        {
            return _context.Folders.Find(id);
        }

        public async Task<List<Folder>?> GetSubFoldersById(int id, int userId)
        {
            var results = from f in _context.Folders
                          join d in _context.Drives on f.DriveId equals d.Id
                          where d.UserId == userId && f.ParrentFolderId == id
                          select f;
            return await results.ToListAsync();
        }

        public async Task<List<Item>?> GetFilesById(int id, int userId)
        {
            var results = from i in _context.Items
                          join d in _context.Drives on i.DriveId equals d.Id
                          where d.UserId == userId && i.FolderId == id
                          select i;
            return await results.ToListAsync();
        }

        public async Task<Folder> Insert(Folder folder)
        {
            await _context.Folders.AddAsync(folder);
            await _context.SaveChangesAsync();
            return folder;
        }

        public async Task<Folder> Update(Folder folder)
        {
            _context.ChangeTracker.Clear();
            _context.Folders.Update(folder);
            await _context.SaveChangesAsync();
            return folder;
        }

        public async Task Delete(Folder folder) 
        {
            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExisted(int id)
        {
            var folderInDb = await _context.Folders.FindAsync(id);
            return folderInDb != null;
        }

        public async Task<bool> HadNameAndDriveIdAndParrentFolderId(string name, int driveId, int? parrentFolderId)
        {
            var folderInDb = await _context.Folders.SingleOrDefaultAsync(f => f.Name == name && f.DriveId == driveId && f.ParrentFolderId == parrentFolderId);
            return folderInDb != null;
        }

    }
}
