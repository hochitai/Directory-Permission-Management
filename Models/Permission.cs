namespace DirectoryPermissionManagement.Models
{
    public class Permission
    {
        public int UserId { get; set; }
        public int FolderId { get; set; }
        public int ItemId { get; set; }
        public int RoleId {  get; set; }
    }
}
