using Microsoft.EntityFrameworkCore;
using DirectoryPermissionManagement.Models;

namespace DirectoryPermissionManagement.Data
{
    public class FolderContext : DbContext
    {
        public DbSet<Folder> Folders { get; set; }
        public FolderContext(DbContextOptions<FolderContext> options) : base(options) {}
    }
}
