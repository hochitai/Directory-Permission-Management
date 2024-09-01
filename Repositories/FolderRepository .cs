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
            return await _context.Folders.FindAsync(id);
        }

        public async Task<List<Folder>?> GetFoldersByDriveId(int driveId)
        {
            return await _context.Folders.Where(d => d.DriveId == driveId && d.ParrentFolderId == 0).ToListAsync();
        }

        public async Task<List<Folder>?> GetSubFoldersById(int folderId, int userId)
        {
            var results = from f in _context.Folders
                          join d in _context.Drives on f.DriveId equals d.Id
                          where f.ParrentFolderId == folderId
                          select f;
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
