using Microsoft.EntityFrameworkCore;
using DirectoryPermissionManagement.Models;

namespace DirectoryPermissionManagement.Data
{
    public class PermissionContext : DbContext
    {
        public DbSet<Permission> Permissions { get; set; }
        public PermissionContext(DbContextOptions<PermissionContext> options) : base(options) {}
    }
}
