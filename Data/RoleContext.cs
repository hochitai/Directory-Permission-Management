using Microsoft.EntityFrameworkCore;
using DirectoryPermissionManagement.Models;

namespace DirectoryPermissionManagement.Data
{
    public class RoleContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public RoleContext(DbContextOptions<RoleContext> options) : base(options) {}
    }
}
