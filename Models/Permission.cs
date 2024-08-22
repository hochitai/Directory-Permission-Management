using System.ComponentModel.DataAnnotations;

namespace DirectoryPermissionManagement.Models
{
    public class Permission
    {
        [Key]
        public int UserId { get; set; }
        public int FolderId { get; set; }
        public int ItemId { get; set; }
        public string Role {  get; set; }
    }
}
