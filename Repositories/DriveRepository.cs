using DirectoryPermissionManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DirectoryPermissionManagement.Repositories
{
    public class DriveRepository
    {
        private readonly ApplicationContext _context;

        public DriveRepository(ApplicationContext context)
        {
            _context = context;
        }
        
        public async Task<List<Folder>?> GetFoldersById(int id)
        {
            return await _context.Folders.Where(d => d.DriveId == id && d.ParrentFolderId == 0).ToListAsync();
        }

        public async Task<List<Item>?> GetFilesById(int id)
        {
            return await _context.Items.Where(i => i.DriveId == id && i.FolderId == 0).ToListAsync();
        }

        public async Task<List<Drive>?> GetByUserId(int userId)
        {
            return await _context.Drives.Where(d => d.UserId == userId).ToListAsync();
        }

        public async Task<Drive?> GetById(int id)
        {
            return await _context.Drives.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Drive> Insert(Drive drive)
        {
            await _context.Drives.AddAsync(drive);
            await _context.SaveChangesAsync();
            return drive;
        }

        public async Task<Drive> Update(Drive drive)
        {
            _context.ChangeTracker.Clear();
            _context.Drives.Update(drive);
            await _context.SaveChangesAsync();
            return drive;
        }

        public async Task Delete(Drive drive) 
        {
            _context.Drives.Remove(drive);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasNameAndUserId(string name, int userId)
        {
            var driveInDb = await _context.Drives.FirstOrDefaultAsync(d => d.Name == name && d.UserId == userId);
            return driveInDb != null;
        }
        /*
        public async Task<bool> IsExisted(int id)
        {
            var driveInDb = await _context.Drives.FindAsync(id);
            return driveInDb != null;
        }

        public async Task<bool> IsOwner(int id, int userId)
        {
            var driveInDb = await _context.Drives.FirstOrDefaultAsync(d => d.Id == id && d.UserId == userId);
            return driveInDb != null;
        }
        */
    }
}
