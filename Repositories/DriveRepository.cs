using DirectoryPermissionManagement.Models;

namespace DirectoryPermissionManagement.Repositories
{
    public class DriveRepository
    {
        private readonly ApplicationContext _context;

        public DriveRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Drive? GetById(int id)
        {
            return _context.Drives.FirstOrDefault(x => x.Id == id);
        }

        public List<Drive>? GetByUserId(int userId)
        {
            return _context.Drives.Where(x => x.UserId == userId).ToList();
        }

        public Drive Insert(Drive drive)
        {
            _context.Drives.Add(drive);
            _context.SaveChanges();
            return drive;
        }

        public Drive Update(Drive drive)
        {
            _context.Drives.Update(drive);
            _context.SaveChanges();
            return drive;
        }

        public void Delete(Drive drive) 
        {
            _context.Drives.Remove(drive);
            _context.SaveChanges();
        }

        public bool IsExisted(Drive drive)
        {
            var driveInDb = _context.Drives.SingleOrDefault(u => u.Name == drive.Name && u.UserId == drive.UserId);
            return driveInDb != null;
        }

        public bool IsExisted(int id)
        {
            var driveInDb = _context.Drives.Find(id);
            return driveInDb != null;
        }

    }
}
