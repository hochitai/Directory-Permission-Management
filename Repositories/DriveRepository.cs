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
            return _context.Drives.SingleOrDefault(d => d.Id == id);
        }

        public List<Drive>? GetByUserId(int userId)
        {
            return _context.Drives.Where(d => d.UserId == userId).ToList();
        }

        public Drive Insert(Drive drive)
        {
            _context.Drives.Add(drive);
            _context.SaveChanges();
            return drive;
        }

        public Drive Update(Drive drive)
        {
            _context.ChangeTracker.Clear();
            _context.Drives.Update(drive);
            _context.SaveChanges();
            return drive;
        }

        public void Delete(Drive drive) 
        {
            _context.Drives.Remove(drive);
            _context.SaveChanges();
        }

        public bool HadNameAndUserId(string name, int userId)
        {
            var driveInDb = _context.Drives.SingleOrDefault(d => d.Name == name && d.UserId == userId);
            return driveInDb != null;
        }

        public bool IsExisted(int id)
        {
            var driveInDb = _context.Drives.Find(id);
            return driveInDb != null;
        }

    }
}
