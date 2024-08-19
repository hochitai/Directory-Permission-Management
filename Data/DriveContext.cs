using Microsoft.EntityFrameworkCore;
using DirectoryPermissionManagement.Models;

namespace DirectoryPermissionManagement.Data
{
    public class DriveContext : DbContext
    {
        public DbSet<Drive> Drives { get; set; }
        public DriveContext(DbContextOptions<DriveContext> options) : base(options) {}
    }
}
