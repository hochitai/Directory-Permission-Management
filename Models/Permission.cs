using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DirectoryPermissionManagement.Models
{
    [Keyless]
    public class Permission
    {
        public int UserId { get; set; }
        public int? DriveId { get; set; }

        public int? FolderId { get; set; }
        public int? ItemId { get; set; }
        public int RoleId {  get; set; }
    }
}
