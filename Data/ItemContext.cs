using Microsoft.EntityFrameworkCore;
using DirectoryPermissionManagement.Models;

namespace DirectoryPermissionManagement.Data
{
    public class ItemContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public ItemContext(DbContextOptions<ItemContext> options) : base(options) {}
    }
}
