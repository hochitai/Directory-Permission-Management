using Microsoft.EntityFrameworkCore;

namespace DirectoryPermissionManagement.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Drive> Drives { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}
